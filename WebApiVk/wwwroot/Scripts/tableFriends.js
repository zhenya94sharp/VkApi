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
            log: '',
            pass: '',
            mes: '',
            id:0
        }
    },
    template:`
    <div> 
    <div id="gridSystemModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="gridModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="gridModalLabel">Для отправки поздравления введите текст, логин и пароль</h4>
            </div>
            <div class="modal-body">
                <div class="container-fluid bd-example-row">
                    <label for="inputMessage">Текст поздравления</label>
                    <textarea class="form-control" v-model="mes" id="inputMessage" rows="3">Введите текст поздравления</textarea>
                    <br>
                    <label for="inputLogin">Логин</label>
                    <input class="form-control" type="text" v-model="log"  id="inputLogin" aria-describedby="loginHelp" placeholder="Введите логин">
                    <small id="loginHelp" class="form-text text-muted">Ваши данные никогда не будут использованы третими лицами.</small>
                    <label for="inputPassword">Пароль</label>
                    <input class="form-control" type="password" v-model="pass" id="inputPassword" placeholder="Введите пароль">
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Отмена</button>
                <button class="btn btn-danger" v-on:click="sendMessage()">Отправить сообщение</button>
            </div>
          </div>
       </div>
    </div>

        <button class="btn btn-danger" v-on:click="friendsLoad()">Загрузить дни рождения друзей</button>

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
                <tr v-for="friend in friendsList">
                <td>{{friend.name}}</td>
                <td>{{convertDate(friend.birthday)}}</td>
                <td>{{friend.idUser}}</td>
                <td v-if="today-new Date(friend.birthday)>0 && today-new Date(friend.birthday)<1000*3600*24">
                <button type="button" class="btn btn-primary btn-lg" v-on:click="SaveId(friend.idUser)" data-toggle="modal" data-target="#gridSystemModal">Отправить поздравление</button>
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
        sendMessage: async function () {
            let json = {
                id: this.id,
                password: this.pass,
                login: this.log,
                message:this.mes
            };

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

        },

        SaveId: function(idUser) {
            let saveId = 0;
            saveId = idUser;
            this.id = saveId;
        }

    }
}




