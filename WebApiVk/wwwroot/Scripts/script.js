import { tableFriends } from "./tableFriends.js";

let vm = new Vue({
    el: "#tf",
    components: {
        "table-friends": tableFriends
    }
});