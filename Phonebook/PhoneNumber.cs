namespace Phonebook;

/// <summary>
/// Номер телефона.
/// </summary>
public class PhoneNumber
{
  /// <summary>
  /// Значение номера телефона.
  /// </summary>
  public string Number { get; }

  /// <summary>
  /// Тип номера телефона.
  /// </summary>
  public PhoneNumberType Type { get; }

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="number">Значение номера телефона.</param>
  /// <param name="type">Тип номера телефона.</param>
  public PhoneNumber(string number, PhoneNumberType type)
  {
    if(!string.IsNullOrEmpty(number))
      this.Number = number;
    else
      throw new ArgumentNullException(nameof(number));
		if (!string.IsNullOrEmpty(number)) 
      this.Type = type;
    else
      throw new ArgumentNullException(nameof(type));
  }
}