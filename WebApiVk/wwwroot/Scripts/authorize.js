export let dataAuthorize = {
    data() {
        return {
            log: '',
            pass: '',
            mes: ''
        }
    },
    template:`
    <div> 
            <h3>Для отправки поздравления введите текст, логин и пароль</h3>
            <label for="inputMessage">Текст поздравления</label>
            <textarea v-model="mes" id="inputMessage">Введите текст поздравления</textarea>
    </br>

            <label for="inputLogin">Login</label>
            <input type="text" v-model="log"  id="inputLogin" aria-describedby="loginHelp" placeholder="Введите логин">
            <small id="loginHelp" class="form-text text-muted">Ваши данные никогда не будут использованы третими лицами.</small>

            <label for="inputPassword">Пароль</label>
            <input type="password" v-model="pass" id="inputPassword" placeholder="Введите пароль">
    </div>

    <button class="btn btn-danger" v-on:click="sendMessage">Отправить поздравление</button>
    `,
    methods: {
        sendMessage: async function (event, idUser) {
            let json = {
                id: idUser,
                password: this.pass,
                login: this.log,
                message: this.mes
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
                alert("Поздравление отправлено" + this.pass + this.log);
            } else {
                alert("При загрузке данных произошла ошибка " + response.status);
            }

        }
    }
}