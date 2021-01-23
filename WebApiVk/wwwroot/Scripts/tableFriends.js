let options = {
    month: 'long',
    weekday: 'long',
    day: 'numeric'
};

export let tableFriends = {
    data(){
        return{
            friendsList: [],
            today: new Date(Date.now()),
            log: '1',
            pass:'2'
        }
    },
    template:`
    <div> 
        <div>
            <label for="inputEmail">Email</label>
            <input type="text" v-model="log"  id="inputEmail" aria-describedby="emailHelp" placeholder="Введите логин">
 
            <small id="emailHelp" class="form-text text-muted">Ваши данные никогда не будут использованы третими лицами.</small>

            <label for="inputPassword">Пароль</label>
            <input type="password" v-model="pass" id="inputPassword" placeholder="Введите пароль">
          </div>

        <button class="btn btn-danger" v-on:click="friendsLoad">Загрузить дни рождения друзей</button>

        <div v-if="friendsList!=0" class="list-group">
            <table class='table table-striped table-dark'>
            <thead>
                <tr>
                <th scope='col'>Имя</th>
                <th scope='col'>День рождения</th>
                <th scope='col'>Id</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="friend in friendsList" v-if="today-new Date(friend.birthday)>-1000*3600*24*30 && today-new Date(friend.birthday)<1000*3600*24">
                <td>{{friend.name}}</td>
                <td>{{convertDate(friend.birthday)}}</td>
                <td>{{friend.idUser}}</td>
                <td v-if="today-new Date(friend.birthday)>0 && today-new Date(friend.birthday)<1000*3600*24">
                <button class="btn btn-danger" v-on:click="sendMessage($event, friend.idUser)">Отправить поздравление</button>
                </td>
                </tr>
            </tbody> 
        </table>
        </div>
    </div>   
    `,
    methods: {
        friendsLoad: async function() {
            let response = await fetch("https://localhost:44335/api/Vk",
                {
                    method: 'GET',
                    headers: {
                        accept: 'application/json',
                        'Content-Type': 'application/json;charset=utf-8'
                    }
                });

            if (response.ok == true) {
                this.friendsList = await response.json();
                alert("Получены данные о друзьях");
            } else {
                alert("При загрузке данных произошла ошибка " + response.status);
            }
        },
        convertDate: function(date) {
            let birthday = new Date(date).toLocaleString("ru", options);
            return birthday;
        },
        sendMessage: async function (event, idUser) {
            let json = {
                id: idUser,
                password: this.pass,
                login: this.log
            };
            //console.dir(json);
            let jsonString = JSON.stringify(json);

            console.dir(jsonString);

            let response = await fetch("https://localhost:44335/api/Vk",
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json;charset=utf-8'
                    },
                    body: jsonString
                });

            if (response.ok == true) {
                alert("Поздравление отправлено"+this.pass+this.log);
            } else {
                alert("При загрузке данных произошла ошибка " + response.status);
            }

        }
    }
}




