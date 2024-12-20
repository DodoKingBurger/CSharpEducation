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

		[Test]
		public void GetSubscriber_WithNullId_ThrowArgumentNullException()
		{
			string subscriberName = "Biba";
			Guid guid = Guid.Parse("00000000-0000-0000-0000-000000000000");
			var OldSub = new Subscriber(guid, subscriberName, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(OldSub);
			
			Assert.Throws<ArgumentNullException>(() => this.phonebook.GetSubscriber(guid));
		}

		[Test]
		public void GetSubscriber_WithEmptyPhonebook_ThrowNullReferenceException()
		{
			Guid subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			
			Assert.Throws<NullReferenceException>(() => this.phonebook.GetSubscriber(subscriberId));
		}

		[Test]
		public void GetSubscriber_WithNonExistentSubscriber_ThrowArgumentNullException()
		{
			string subscriberName = "Biba";
			Guid guid = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			var OldSub = new Subscriber(guid, subscriberName, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(OldSub);
			Guid subscriberId = Guid.Parse("B58B3851-9F5F-4CB8-BC0B-64BB42794EA7");
			
			Assert.Throws<InvalidOperationException>(() => this.phonebook.GetSubscriber(subscriberId));
		}

		[Test]
		public void GetSubscriber_FindSubscriber_GetSuccessfuly()
		{
			string subscriberName = "Biba";
			Guid guid = Guid.Parse("B58B3851-9F5F-4CB8-BC0B-64BB42794EA7");
			var expectedSubs = new Subscriber(guid, subscriberName, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(expectedSubs);
			Guid subscriberId = Guid.Parse("B58B3851-9F5F-4CB8-BC0B-64BB42794EA7");
			
			Assert.That(this.phonebook.GetSubscriber(guid).Id, Is.EqualTo(expectedSubs.Id));
		}

		[Test]
		public void GetSubscriber_WithTwoIdenticalSubscribers_ThrowInvalidOperationException()
		{
			string subscriberName = "Biba";
			Guid guid = Guid.Parse("B58B3851-9F5F-4CB8-BC0B-64BB42794EA7");
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
			Assert.Throws<NullReferenceException>(() => this.phonebook.GetAll());
		}

		[Test]
		public void GetAll_ListSubscriber_GetAllList()
		{
			string subscriberName = "Biba";
			Guid guid1 = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD013");
			Guid guid2 = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			var Sub1 = new Subscriber(guid1, subscriberName, new List<PhoneNumber>());
			var Sub2 = new Subscriber(guid2, subscriberName, new List<PhoneNumber>());
			List<Subscriber> listSubcribers = new List<Subscriber>() { Sub1, Sub2 };
			this.phonebook = new Phonebook(listSubcribers);
			
			Assert.That(this.phonebook.GetAll().Count, Is.EqualTo(2));
		}

		[Test]
		public void AddSubscriber_NewSubscriber_AddedSuccessfully()
		{
			Guid guid = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD024");
			var expectedSubscriber = new Subscriber(guid, "Ali Abu Aga Nasral", new List<PhoneNumber>());
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

		[Test]
		public void AddNumberToSubscriber_NoValidingPhoneNumber_ThrowInvalidOperationException()
		{
			Guid subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			string subscriberName = "Egor";
			var expectedSubscriber = new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(expectedSubscriber);
			PhoneNumber phoneNumber = new PhoneNumber("+7905443534345345345", PhoneNumberType.Work);

			Assert.Throws<InvalidOperationException>(() => this.phonebook.AddNumberToSubscriber(expectedSubscriber, phoneNumber));
		}

		[Test]
		public void AddNumberToSubscriber_AddingValidingPhoneNumber_SubscriberReceivesPhoneNumber()
		{
			Guid subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			string subscriberName = "Egor";
			var expectedSubscriber = new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(expectedSubscriber);
			PhoneNumber phoneNumber = new PhoneNumber("+7(800)555-3535", PhoneNumberType.Personal);
			phonebook.AddNumberToSubscriber(expectedSubscriber,phoneNumber);
			Assert.That(phonebook.GetSubscriber(subscriberId).PhoneNumbers[0], Is.EqualTo(phoneNumber));
		}

		[Test]
		public void AddNumberToSubscriber_WithTwoIdenticalSubscribers_ThrowInvalidOperationException()
		{
			Guid subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			string subscriberName = "Biba";
			Subscriber expectedSubs = new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>());
			List<Subscriber> listSubscriber = new List<Subscriber>()
			{
				expectedSubs, expectedSubs
			};
			this.phonebook = new Phonebook(listSubscriber);
			PhoneNumber phoneNumber = new PhoneNumber("+7(800)555-3535", PhoneNumberType.Personal);
			
			Assert.Throws<InvalidOperationException>(() => this.phonebook.AddNumberToSubscriber(expectedSubs, phoneNumber));
		}

		[Test]
		public void AddNumberToSubscriber_WithEmptySubscribers_ThrowNullReferenceException()
		{
			Subscriber expectedSubs = null!;
			PhoneNumber phoneNumber = new PhoneNumber("+7(800)555-3535", PhoneNumberType.Personal);
			
			Assert.Throws<NullReferenceException>(() => this.phonebook.AddNumberToSubscriber(expectedSubs, phoneNumber));
		}

		[Test]
		public void AddNumberToSubscriber_WithEmptyPhoneNumber_ThrowArgumentNullException()
		{
			Guid subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			string subscriberName = "Biba";
			Subscriber expectedSubs = new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(expectedSubs);
		
			Assert.Throws<ArgumentNullException>(() => this.phonebook.AddNumberToSubscriber(expectedSubs, 
				new PhoneNumber("",PhoneNumberType.Personal)));
		}

		[Test]
		public void RenameSubscriber_ChangeNameSubcriberOnIvan_SubscriberWillBecomIvan()
		{
			Guid subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD084");
			var expectedSubscriber = new Subscriber(subscriberId, "Egor", new List<PhoneNumber>());
			this.phonebook.AddSubscriber(expectedSubscriber);
			this.phonebook.RenameSubscriber(expectedSubscriber, "Ivan");
			
			Assert.That(this.phonebook.GetSubscriber(subscriberId).Name, Is.EqualTo("Ivan"));
		}

		[Test]
		public void RenameSubscriber_WithEmptySubcriber_ThrowNullReferenceException()
		{
			Subscriber expectedSubs = null!;
			
			Assert.Throws<ArgumentNullException>(() => this.phonebook.RenameSubscriber(expectedSubs, "Ivan"));
		}

		[Test]
		public void RenameSubscriber_WithSubcriberNotInPhoneBook_ThrowArgumentException()
		{
			Guid subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			string subscriberName = "Biba";
			Subscriber expectedSubs1 = new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>());
			subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD013");
			subscriberName = "Boba";
			Subscriber expectedSubs2 = new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(expectedSubs1);

			Assert.Throws<ArgumentException>(() => this.phonebook.RenameSubscriber(expectedSubs2, "Ivan"));
		}

		[Test]
		public void RenameSubscriber_WithEmptyName_ThrowNullReferenceException()
		{
			Guid subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			string subscriberName = "Biba";
			Subscriber expectedSubs = new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>());

			Assert.Throws<NullReferenceException>(() => this.phonebook.RenameSubscriber(expectedSubs, string.Empty));
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

		[Test]
		public void UpdateSubscriber_WithNewSubscriberEmpty_ThrowArgumentNullException()
		{
			string subscriberName = "Biba";
			Guid guid = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			var OldSub = new Subscriber(guid, subscriberName, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(OldSub);
			
			Assert.Throws<ArgumentNullException>(() => this.phonebook.UpdateSubscriber(OldSub, null));
		}

		[Test]
		public void UpdateSubscriber_WithSubscriberNotFound_ThrowArgumentException()
		{
			string subscriberName = "Biba";
			Guid guid = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			var OldSub = new Subscriber(guid, subscriberName, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(OldSub);
			string subscriberName2 = "Boba";
			Guid guid2 = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD011");
			var NewSub = new Subscriber(guid2, subscriberName2, new List<PhoneNumber>());
			
			Assert.Throws<ArgumentException>(() => this.phonebook.UpdateSubscriber(NewSub, OldSub));
		}

		[Test]
		public void DeleteSubscriber_DeletetSubscribe_SubscribeIsDeletet()
		{
			Guid subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			string subscriberName = "Biba";
			Subscriber expectedSubs = new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(expectedSubs);
			Guid subscriberId2 = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD011");
			string subscriberName2 = "Boba";
			Subscriber expectedSubs2 = new Subscriber(subscriberId2, subscriberName2, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(expectedSubs2);
			this.phonebook.DeleteSubscriber(expectedSubs);
			
			Assert.False(this.phonebook.GetAll().Contains(expectedSubs));
		}

		[Test]
		public void DeleteSubscriber_WithEmptyPhonebook_ThrowNullReferenceException()
		{
			Guid subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			string subscriberName = "Biba";
			Subscriber expectedSubs = new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>());

			Assert.Throws<NullReferenceException>(() => this.phonebook.DeleteSubscriber(expectedSubs));
		}

		[Test]
		public void DeleteSubscriber_WithEmptySubscriber_ThrowNullReferenceException()
		{
			Subscriber expectedSubs = null!;

			Assert.Throws<NullReferenceException>(() => this.phonebook.DeleteSubscriber(expectedSubs));
		}

		[Test]
		public void DeleteSubscriber_WithSubscriberIsNotList_ThrowArgumentException()
		{
			Guid subscriberId = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD014");
			string subscriberName = "Biba";
			Subscriber expectedSubs = new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>());
			Guid subscriberId2 = Guid.Parse("29777CA3-C07D-4545-9328-6E87579AD011");
			string subscriberName2 = "Boba";
			Subscriber expectedSubs2 = new Subscriber(subscriberId2, subscriberName2, new List<PhoneNumber>());
			this.phonebook.AddSubscriber(expectedSubs2);

			Assert.Throws<ArgumentException>(() => this.phonebook.DeleteSubscriber(expectedSubs));
		}
	}
}