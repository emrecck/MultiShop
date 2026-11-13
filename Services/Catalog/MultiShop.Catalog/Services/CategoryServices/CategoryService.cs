using System;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Mapping;
using MultiShop.Catalog.Settings;
using Microsoft.Extensions.Options;

namespace MultiShop.Catalog.Services.CategoryServices;

public class CategoryService : ICategoryService
{
    private readonly IMongoDatabase _mongodb;
    private readonly IMongoCollection<Category> _categories;
    private readonly CategoryMapper _mapper;

    public CategoryService(CategoryMapper mapper, IOptions<DatabaseSettings> databaseSettingsOptions)
    {
        _mapper = mapper;
        var databaseSettings = databaseSettingsOptions.Value;
        var mongoClient = new MongoClient(databaseSettings.ConnectionString);
        _mongodb = mongoClient.GetDatabase(databaseSettings.DatabaseName);
        _categories = _mongodb.GetCollection<Category>(databaseSettings.CategoryCollectionName);
    }

    public async Task<string> CreateCategoryAsync(
        CreateCategoryDto category,
        CancellationToken cancellationToken)
    {
        var newCategory = _mapper.ToEntity(category);
        await _categories.InsertOneAsync(newCategory, cancellationToken: cancellationToken);
        return newCategory.Id;
    }

    public async Task<bool> DeleteCategoryAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _categories.DeleteOneAsync(
            category => category.Id == id,
            cancellationToken: cancellationToken);
        return result.DeletedCount > 0;
    }

    public async Task<IEnumerable<ResultCategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        var categories = await _categories.Find(_ => true).ToListAsync(cancellationToken);
        return categories.Select(c => _mapper.ToResultDto(c));
    }

    public async Task<GetByIdCategoryDto?> GetCategoryByIdAsync(
        string id,
        CancellationToken cancellationToken)
    {
        var category = await _categories.Find(c => c.Id == id).FirstOrDefaultAsync(cancellationToken);
        return category is null ? null : _mapper.ToGetByIdDto(category);
    }

    public async Task<bool> UpdateCategoryAsync(
        UpdateCategoryDto category,
        CancellationToken cancellationToken)
    {
        var filter = Builders<Category>.Filter.Eq(c => c.Id, category.Id);
        var update = Builders<Category>.Update
            .Set(c => c.Name, category.Name);
        var result = await _categories.UpdateOneAsync(
            filter,
            update,
            cancellationToken: cancellationToken);
        return result.MatchedCount > 0;
    }
}
