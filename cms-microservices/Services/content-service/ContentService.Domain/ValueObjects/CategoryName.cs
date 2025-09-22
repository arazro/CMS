using System;

namespace ContentService.Domain.ValueObjects;

public readonly struct CategoryName 
{
    public string Value { get; }

    public CategoryName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Category name cannot be empty.");

        if (value.Length > 50)
            throw new ArgumentException("Category name cannot exceed 50 characters.");

        Value = value.Trim();
    }

    public override string ToString() => Value;

    public static implicit operator string(CategoryName category) => category.Value;
    public static explicit operator CategoryName(string value) => new CategoryName(value);

    public override bool Equals(object? obj) => obj is CategoryName other && Equals(other);
    public bool Equals(CategoryName other) => Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
}
