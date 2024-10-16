using System.Net.Http.Headers;

namespace Phonebook;

/// <summary>
/// Телефонная книга.
/// </summary>
public class Phonebook
{
  #region Поля и свойства

  /// <summary>
  /// Абоненты в книге.
  /// </summary>
  private readonly List<Subscriber> subscribers;

  #endregion

  #region Методы

  /// <summary>
  /// Получить абонента из книги.
  /// </summary>
  /// <param name="id">ИД абонента.</param>
  /// <returns>Найденный абонент в книге.</returns>
  public Subscriber GetSubscriber(Guid id)
  {
    if (this.subscribers.Any())
    {
      if (id != Guid.Empty)
        return this.subscribers.Single(s => s.Id == id);
      else
        throw new ArgumentNullException($"Передан пустой {id.GetType}");
    }
    else
      throw new NullReferenceException("Список подписчиков пуст");
  }

  /// <summary>
  /// Получить всех абонентов.
  /// </summary>
  /// <returns>Список всех абонентов.</returns>
  public IEnumerable<Subscriber> GetAll()
  {
    if (this.subscribers.Any())
    {
      return this.subscribers;
    }
    else
      throw new NullReferenceException("Список пуст");
  }

  /// <summary>
  /// Добавить абонента в книгу.
  /// </summary>
  /// <param name="subscriber">Абонент, которого нужно добавить.</param>
  /// <exception cref="InvalidOperationException">Возникает, если абонент уже существует в книге.</exception>
  public void AddSubscriber(Subscriber subscriber)
  {
    if(subscriber != null)
    {

      if (this.subscribers.Contains(subscriber)&& this.subscribers.Where(s => s.Id == subscriber.Id).ToList().Count != 0)
        throw new InvalidOperationException("Unable to add subscriber. Subscriber exists");
      
      PhoneNumberValidator.ValidateList(subscriber.PhoneNumbers);

      this.subscribers.Add(subscriber);
    }
    else
      throw new ArgumentNullException($"Подписчик {subscriber} не заполнен");
  }

  /// <summary>
  /// Добавить номер для абонента.
  /// </summary>
  /// <param name="subscriber">Абонент, которому нужно добавить номер.</param>
  /// <param name="number">Добавляемый номер абонента.</param>
  public void AddNumberToSubscriber(Subscriber subscriber, PhoneNumber number)
  {
    if (this.subscribers.Any())
    {
      if (!string.IsNullOrEmpty(number.Number))
      {
        if (this.subscribers.Contains(subscriber))
        {
          var newNumbers = new List<PhoneNumber>(subscriber.PhoneNumbers)
          {
              number
          };
          var subscriberWithNewNumber = new Subscriber(subscriber.Id, subscriber.Name, newNumbers);

          this.UpdateSubscriber(subscriber, subscriberWithNewNumber);
        }
        else
          throw new ArgumentException($"Такой подподписчик {subscriber.Name} отсутсвует");
      }
      else throw new ArgumentException("Новый номер телефона не указан");
    }
    else
      throw new NullReferenceException("Список пуст");
  }

  /// <summary>
  /// Сменить имя абонента.
  /// </summary>
  /// <param name="subscriber">Абонент, которому нужно сменить имя.</param>
  /// <param name="newName">Новое имя абонента.</param>
  public void RenameSubscriber(Subscriber subscriber, string newName)
  {
    if (subscriber != null)
    {
      if (this.subscribers.Any())
      {
        if (this.subscribers.Contains(subscriber))
        {
          if (!string.IsNullOrEmpty(newName))
          {
            var subscriberWithNewName = new Subscriber(subscriber.Id, newName, subscriber.PhoneNumbers);
            this.UpdateSubscriber(subscriber, subscriberWithNewName);
          }
          else
            throw new ArgumentException("Новое имя не указанно");
        }
        else throw new ArgumentException($"Такого {subscriber.Name} нету в списке");
      }
      else
        throw new NullReferenceException("Список пуст");
    }
    else
      throw new ArgumentNullException("Подписчик пуст");
  }

  /// <summary>
  /// Обновить абонента в книге.
  /// </summary>
  /// <param name="oldSubscriber">Старый абонент.</param>
  /// <param name="newSubscriber">Новый абонент.</param>
  public void UpdateSubscriber(Subscriber oldSubscriber, Subscriber newSubscriber)
  {
    if(oldSubscriber != null && newSubscriber != null)
    {
      if (this.subscribers.Any())
      {
        if (oldSubscriber.Id != newSubscriber.Id)
          throw new ArgumentException($"{oldSubscriber.Name} и у {newSubscriber.Name} не совпадают ID");
        if (this.subscribers.Contains(oldSubscriber))
        {
          var foundSubscriber = this.GetSubscriber(oldSubscriber.Id);
          int foundSubscriberPlace = this.subscribers.FindIndex(s => s.Id == foundSubscriber.Id);
          this.subscribers[foundSubscriberPlace] = newSubscriber;
        }
        else
          throw new ArgumentException($"Подписчик {oldSubscriber.Name} отсутсвует в списке");
      }
      else
        throw new NullReferenceException("Список пуст");
    }
    else
      throw new ArgumentNullException("Данные изменяймого подписчика утеряны");
  }

  /// <summary>
  /// Удалить абонента в книге.
  /// </summary>
  /// <param name="subscriberToDelete">Абонент, которого нужно удалить из книги.</param>
  public void DeleteSubscriber(Subscriber subscriberToDelete)
  {
    if (this.subscribers.Any())
    {
      if (subscriberToDelete == null)
        throw new NullReferenceException("Подписчик пустой");
      if (!this.subscribers.Contains(subscriberToDelete))
        throw new ArgumentException("Данный подписчик отсутсвует в списке");
      this.subscribers.Remove(subscriberToDelete);
    }
		else
			throw new NullReferenceException("Список пуст");
	}

	/// <summary>
	/// Очистить телефонную книгу.
	/// </summary>
	/// <param name="subscriberToDelete">Абонент, которого нужно удалить из книги.</param>
	internal void ClearListSubscriber()
	{
		this.subscribers.Clear();
	}

	#endregion

	#region Конструкторы

	/// <summary>
	/// Конструктор.
	/// </summary>
	public Phonebook()
      : this(new List<Subscriber>())
  {
  }

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="subscribers">Список абонентов для инициализации книги.</param>
  public Phonebook(List<Subscriber> subscribers)
  {
    this.subscribers = subscribers;
  }

  #endregion
}
