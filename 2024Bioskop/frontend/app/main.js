import { Application } from "./application.js";

let projekcijeFetch=await fetch("https://localhost:7080/Ispit/VratiProjekcije").then(r=>r.json());
const app=new Application(projekcijeFetch);
app.draw(document.body);