using System;
using System.Linq;
using System.Reflection;

namespace TASK3_Business.Helpers {
  public static class Mapper {
    public static TEntity? MapToEntity<TDto, TEntity>(TDto dto)
        where TEntity : new() {
      if (dto == null) return default;

      TEntity entity = new();
      MapProperties(dto, entity);
      return entity;
    }

    public static TDto? MapToDto<TEntity, TDto>(TEntity entity)
        where TDto : new() {
      if (entity == null) return default;

      TDto dto = new();
      MapProperties(entity, dto);
      return dto;
    }

    private static void MapProperties<TSource, TDestination>(TSource source, TDestination destination) {
      var sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
      var destinationProperties = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

      foreach (var sourceProperty in sourceProperties) {
        var destinationProperty = destinationProperties.FirstOrDefault(prop =>
            prop.Name == sourceProperty.Name &&
            prop.PropertyType == sourceProperty.PropertyType);

        if (destinationProperty != null && destinationProperty.CanWrite) {
          var value = sourceProperty.GetValue(source);
          if (value == null && IsNullableType(destinationProperty.PropertyType)) {
            destinationProperty.SetValue(destination, null);
          }
          else if (value != null) {
            destinationProperty.SetValue(destination, value);
          }
        }
      }
    }

    private static bool IsNullableType(Type type) {
      return !type.IsValueType || type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
  }
}