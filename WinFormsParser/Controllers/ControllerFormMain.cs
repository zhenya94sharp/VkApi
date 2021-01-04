using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                });*/

                await api.AuthorizeAsync(new ApiAuthParams()
                {
                    AccessToken = "31f3be54127b0255a1ed07fffba1c69618e14270123d8ba517da0e2a564c53408b811d997780eeb82f734"
                });
            /*}
            catch (Exception e)
            {
                MessageBox.Show("Не удалось подключиться, проверьте правильность введённых данных\n" + e.Message);
                return;
            }*/
            MessageBox.Show($"Авторизация пройдена ваш токен {api.Token}");
        }

        public VkCollection<User> GetCollectionFriends()
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

            dbManager.AddFriendsToDb(friends);
        }




    }
}
