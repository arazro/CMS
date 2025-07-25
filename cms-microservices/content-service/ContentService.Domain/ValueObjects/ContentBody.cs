using System;

namespace ContentService.Domain.ValueObjects;

public readonly struct ContentBody
{
    public string Value { get; }

    public ContentBody(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Body cannot be empty.");

        Value = value.Trim();
    }

    public override string ToString() => Value;

    public static implicit operator string(ContentBody body) => body.Value;
    public static explicit operator ContentBody(string value) => new ContentBody(value);
}
