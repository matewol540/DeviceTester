using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DeviceTester.Models;
using Newtonsoft.Json;
using Plugin.CloudFirestore;

namespace DeviceTester.Helper
{
    public class FirebaseConnector
    {
        private IFirestore client;
        public FirebaseConnector()
        {
            client = CrossCloudFirestore.Current.Instance;
        }
        public async Task<List<UserModel>> GetUsersAsync(int counter)
        {
            return (await client
                     .Collection("Users")
                     .LimitTo(counter)
                     .GetAsync())
                     .Documents.Select(new Func<IDocumentSnapshot, UserModel>(x =>
                     {
                         return x.ToObject<UserModel>();
                     })).ToList<UserModel>();
        }

        internal async void UpdateAvatars()
        {
            var models = (await client
                     .Collection("Users")
                     .GetAsync())
                     .Documents.Select(x => x.Id).ToList();


            models.ForEach(x =>
            {
                var value = new Random().Next(0,50);
                var typeNMBR = new Random().Next(2, 4);
                var type = typeNMBR % 2 == 0 ? "women" : "men";

                Thread.Sleep(1);
                var tempUrl = $"https://randomuser.me/api/portraits/{type}/{value}.jpg";
                client.Collection("Users").Document(x).UpdateAsync(new FieldPath("avatar"), tempUrl);

            });
        }
    }
}
