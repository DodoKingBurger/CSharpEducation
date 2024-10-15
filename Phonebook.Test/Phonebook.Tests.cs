using System;
using System.Net.Http.Headers;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Phonebook.Test
{
	public class Tests
	{
		private Phonebook phonebook;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			this.phonebook = new Phonebook();
		}

		[TearDown]
		public void TearDown()
		{
			this.phonebook.ClearListSubscriber();
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			this.phonebook = null;
		}

		[TestCase("00000000-0000-0000-0000-000000000000")]
		public void GetSubscriber_WithNullId_ThrowInvalidOperationException(string Id)
		{
			Guid subscriberId = Guid.Parse(Id);
			Assert.Throws<InvalidOperationException>(() => this.phonebook.GetSubscriber(subscriberId));
		}

		[TestCase("B58B3851-9F5F-4CB8-BC0B-64BB42794EA7")]
		public void GetSubscriber_WithNonExistentSubscriber_ThrowArgumentNullException(string Id)
		{
			string subscriberName = "Biba";
			Guid guid = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			var OldSub = new Subscriber(guid, subscriberName, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(OldSub);
			Guid subscriberId = Guid.Parse(Id);
			Assert.Throws<InvalidOperationException>(() => this.phonebook.GetSubscriber(subscriberId));
		}

		[TestCase("B58B3851-9F5F-4CB8-BC0B-64BB42794EA7")]
		public void GetSubscriber_FindSubscriber_GetSuccessfuly(string Id)
		{
			string subscriberName = "Biba";
			Guid guid = Guid.Parse(Id);
			var expectedSubs = new Subscriber(guid, subscriberName, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(expectedSubs);
			Guid subscriberId = Guid.Parse(Id);
			Assert.That(this.phonebook.GetSubscriber(guid).Id, Is.EqualTo(expectedSubs.Id));
		}

		[TestCase("B58B3851-9F5F-4CB8-BC0B-64BB42794EA7")]
		public void GetSubscriber_WithTwoIdenticalSubscribers_ThrowInvalidOperationException(string Id)
		{
			string subscriberName = "Biba";
			Guid guid = Guid.Parse(Id);
			Subscriber expectedSubs = new Subscriber(guid, subscriberName, new List<PhoneNumber>());
			List<Subscriber> listSubscriber = new List<Subscriber>()
			{
				expectedSubs, expectedSubs
			};
			this.phonebook = new Phonebook(listSubscriber);
			Assert.Throws<InvalidOperationException>(() => this.phonebook.GetSubscriber(guid));
		}

		[Test]
		public void GetAll_WithEmptyListSubscriber_GetEmptyList()
		{
			Assert.IsEmpty(this.phonebook.GetAll());
		}

		[TestCase("29777CA3-C07D-4545-9328-6E87579AD084", "Egor")]
		[TestCase("29777CA3-C07D-4545-9328-6E87579AD014", "Ali Abu Aga Nasr")]
		[TestCase("29777CA3-C07D-4545-9328-6E87579AD024", "Egor Egorov")]
		public void AddSubscriber_NewSubscriber_AddedSuccessfully(string strId, string subscriberName)
		{
			Guid guid = Guid.Parse(strId);
			var expectedSubscriber = new Subscriber(guid, subscriberName, new List<PhoneNumber>());

			this.phonebook.AddSubscriber(expectedSubscriber);

			Assert.That(this.phonebook.GetSubscriber(guid), Is.EqualTo(expectedSubscriber));
		}

		[Test]
		public void AddSubscriber_ReAddingSubscriber_ThrowInvalidOperationException()
		{
			Guid subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			string subscriberName = "Egor";
			var expectedSubscriber = new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>());

			this.phonebook.AddSubscriber(expectedSubscriber);

			Assert.Throws<InvalidOperationException>(() => this.phonebook.AddSubscriber(expectedSubscriber));
		}

		[TestCase("88005553535", PhoneNumberType.Personal)]
		[TestCase("460000", PhoneNumberType.Work)]
		public void AddNumberToSubscriber_AddingPhoneNumber_SubscriberReceivesPhoneNumber(string number, PhoneNumberType numberType)
		{
			Guid subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			string subscriberName = "Egor";
			var expectedSubscriber = new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(expectedSubscriber);
			PhoneNumber phoneNumber = new PhoneNumber(number, numberType);
			this.phonebook.AddNumberToSubscriber(expectedSubscriber, phoneNumber);
			Assert.That(this.phonebook.GetSubscriber(subscriberId).PhoneNumbers[0], Is.EqualTo(phoneNumber));
		}

		[TestCase("+78005553535", PhoneNumberType.Personal)]
		[TestCase("46-00-00", PhoneNumberType.Work)]
		public void AddNumberToSubscriber_AddingPhoneNumberWithSpecialCharacters_SubscriberReceivesPhoneNumber(string number, PhoneNumberType numberType)
		{
			Guid subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			string subscriberName = "Egor";
			var expectedSubscriber = new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(expectedSubscriber);
			PhoneNumber phoneNumber = new PhoneNumber(number, numberType);
			this.phonebook.AddNumberToSubscriber(expectedSubscriber, phoneNumber);
			Assert.That(this.phonebook.GetSubscriber(subscriberId).PhoneNumbers[0], Is.EqualTo(phoneNumber));
		}

		[TestCase("88005553535", PhoneNumberType.Personal)]
		[TestCase("460000", PhoneNumberType.Work)]
		public void AddNumberToSubscriber_WithTwoIdenticalSubscribers_ThrowInvalidOperatiomException(string number, PhoneNumberType numberType)
		{
			Guid subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			string subscriberName = "Biba";
			Subscriber expectedSubs = new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>());
			List<Subscriber> listSubscriber = new List<Subscriber>()
			{
				expectedSubs, expectedSubs
			};
			this.phonebook = new Phonebook(listSubscriber);
			PhoneNumber phoneNumber = new PhoneNumber(number, numberType);
			Assert.Throws<InvalidOperationException>(() => this.phonebook.AddNumberToSubscriber(expectedSubs, phoneNumber));
		}















		[Test]
		public void UpdateSubscriber_UpdateDataSubscriber_UpdateSuccessfully()
		{
			string subscriberName = "Biba";
			Guid guid = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			var OldSub = new Subscriber(guid, subscriberName, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(OldSub);
			Subscriber NewSub = new Subscriber(OldSub.Id, "Boba", OldSub.PhoneNumbers);
			this.phonebook.UpdateSubscriber(OldSub, NewSub);
			Assert.That(this.phonebook.GetSubscriber(guid).Id, Is.EqualTo(OldSub.Id));
		}
	}
}