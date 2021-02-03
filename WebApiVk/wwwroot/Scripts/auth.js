import { dataAuthorize } from "./authorize.js"

let vm = new Vue({
    el: "#auth",
    components: {
        "authorize": dataAuthorize
    }
});