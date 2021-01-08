using System;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;
using WinFormsParser.Tools;
using Application = System.Windows.Forms.Application;

namespace WinFormsParser
{
    class ControllerFormMain
    {
        private FormMain form;
        static VkApi api = new VkApi();


        public ControllerFormMain(FormMain form)
        {
            this.form = form;
        }

        public async void Autorize()
        {
           /* if (form.textBoxLogin.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (form.textBoxPassword.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (form.textBoxIdApp.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            string login = form.textBoxLogin.Text;
            string password = form.textBoxPassword.Text;
            ulong appId = ulong.Parse(form.textBoxIdApp.Text);

            try
            {
                await api.AuthorizeAsync(new ApiAuthParams
                {
                    Login = login,
                    Password = password,
                    ApplicationId = appId,
                    Settings = Settings.All
                });
                */

                await api.AuthorizeAsync(new ApiAuthParams()
                {
                    AccessToken = "ad9b88cbbc5bb6cce826063247de6f5b31e3af9570c402d5a0281a49ce4495b45d001d2770c57378c56c0"
                });

            /*}
            catch (Exception e)
            {
                MessageBox.Show("Не удалось подключиться, проверьте правильность введённых данных\n" + e.Message);
                return;
            }*/
            MessageBox.Show($"Авторизация пройдена ваш токен {api.Token}");
        }

        private VkCollection<User> GetCollectionFriends()
        {
            string data = "";
            try
            {
                VkCollection<User> friends = api.Friends.Get(new FriendsGetParams()
                {
                    Order = FriendsOrder.Name,
                    Fields = ProfileFields.BirthDate,
                });
                return friends;
            }
            catch (Exception e)
            {
                MessageBox.Show("Не удалось получить данные, авторизуйтесь либо проверьте соединение\n" + e.Message);
                Application.Exit();
                return null;
            }
        }


        public void AddFriendsToDb()
        {
            DbManager dbManager = new DbManager();

            VkCollection<User> friends = GetCollectionFriends();

            dbManager.UpdateFriendsToDb(friends);
        }


    }
}
