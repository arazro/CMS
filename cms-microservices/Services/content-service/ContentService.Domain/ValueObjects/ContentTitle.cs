using System;

namespace ContentService.Domain.ValueObjects;

public readonly struct ContentTitle
{
    public string Value { get; }

    public ContentTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Title cannot be empty.");

        if (value.Length > 150)
            throw new ArgumentException("Title cannot exceed 150 characters.");

        Value = value.Trim();
    }

    public override string ToString() => Value;

    public static implicit operator string(ContentTitle title) => title.Value;
    public static explicit operator ContentTitle(string value) => new ContentTitle(value);
}
