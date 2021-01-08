let options = {
    month: 'long',
    weekday: 'long',
    day: 'numeric'
};

export let tableFriends = {
    data(){
        return{
            friendsList: [],
            date: new Date(Date.now())
        }
    },
    template:`
    <div> 
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
                <tr v-for="friend in friendsList">
                <td>{{friend.name}}</td>
                <td>{{convertDate(friend.birthday)}}</td>
                <td>{{friend.id}}</td>
                <td v-if="date-new Date(friend.birthday)<86400000&&date-new Date(friend.birthday)>-86400000">
                <button class="btn btn-danger" v-on:click="">Отправить поздравление</button>
                <td>
                </tr>
            </tbody> 
        </div>
    </div>   
    `,
    methods:{
        friendsLoad: async function() {
            let response = await fetch("https://localhost:44335/Vk/Get",
                {
                    method: 'GET',
                    headers: {
                        accept: 'application/json',
                        'Content-Type': 'application/json;charset=utf-8'
                    }
                }
            );
            if (response.ok == true) {
                this.friendsList = await response.json()
                alert("Получены данные о друзьях");
            } else {
                alert("При загрузке данных произошла ошибка " + response.status)
            }
        },
        convertDate: function(date) {
            let birthday = new Date(date).toLocaleString("ru", options);
            return birthday;
        }

    }
}




