export class Application{
    constructor(projekcijeFetch)
    {
        this.projekcije=projekcijeFetch;
        console.log(this.projekcije);
        this.polja=[
            {naziv: "Red:", klasa:"red"},
            {naziv: "Broj sedista:", klasa:"brsed"},
            {naziv: "Cena karte:", klasa:"cena"},
            {naziv: "Sifra:", klasa:"sifra"},
        ];

    }

    draw(container)
    {
        const forma= document.createElement("div");
        forma.classList.add("forma");
        this.drawForma(forma);
        container.appendChild(forma);
    }
    

    
    drawForma(container)
    {
        let projekcijaSelect= document.createElement("select");
        projekcijaSelect.classList.add("projekcijaSelect");
        container.appendChild(projekcijaSelect);
        this.projekcije.forEach((p) => {
            let opcija= document.createElement("option");
            opcija.innerHTML=p.naziv + " : " + p.vreme + " - " + p.sala;
            opcija.value= p.id-1;
            projekcijaSelect.appendChild(opcija);
        });

        let glavniDiv=document.createElement("div");
        glavniDiv.classList.add("glavniDiv");
        container.appendChild(glavniDiv);


        let formica= document.createElement("div");
        formica.classList.add("formica");
        glavniDiv.appendChild(formica);
        this.polja.forEach(p =>{
            let lbl= document.createElement("label");
            lbl.innerHTML= p.naziv;
            lbl.classList.add("margin-10");
            formica.appendChild(lbl);

            let input = document.createElement("input");
            input.classList.add(`input-${p.klasa}`, "margin-10");
            formica.appendChild(input);
        });
        

        const btnKupi= document.createElement("input");
        btnKupi.type= "button";
        btnKupi.value="Kupi kartu";
        btnKupi.onclick= ()=> this.onClickKupi();
        btnKupi.classList.add("btnKupi");
        formica.appendChild(btnKupi);



        const karteDiv = document.createElement("div");
        karteDiv.classList.add("karteDiv");
        glavniDiv.appendChild(karteDiv);

        
        projekcijaSelect.onchange = (e) => {
            const index = e.target.value;
            const selektovana = this.projekcije[index];
            this.drawKarte(selektovana, karteDiv);
        };

        this.drawKarte(this.projekcije[0],karteDiv);
        
    }

    async drawKarte(projekcija,karteDiv) {
            let sifraquery= document.querySelector(".input-sifra");
            if (sifraquery) {
                sifraquery.value = projekcija.sifra;
            }

            karteDiv.innerHTML="";

            if (!projekcija) return;
            console.log(projekcija);


            const response = await fetch("https://localhost:7080/Ispit/VratiKarte/"+projekcija.sifra).then(r=>r.json());
            console.log(response);

            let red=projekcija.brojReda;
            let sedista= projekcija.brojSedistaURedu;

            const zauzete = new Set(response.map(k => `${k.red}-${k.sediste}`));

            karteDiv.style.gridTemplateColumns = `repeat(${sedista},1fr)`;
            for(let i=1;i<=red;i++)
            {
                for(let j=1;j<=sedista;j++)
                {
                    let key=`${i}-${j}`;
                    if(zauzete.has(key))
                    {
                        let kartaDiv=document.createElement("div");
                        kartaDiv.classList.add("kartaDiv");
                        kartaDiv.innerHTML="Red: " + i + "; " + "<br />" + "Sediste: " + j + ";";
                        karteDiv.appendChild(kartaDiv);

                        
                        kartaDiv.style.backgroundColor = "red"; 
                    }
                    else{
                        let kartaDiv=document.createElement("div");
                        kartaDiv.classList.add("kartaDiv");
                        kartaDiv.innerHTML="Red: " + i + "; " + "<br />" + "Sediste: " + j + ";";
                        karteDiv.appendChild(kartaDiv);

                         kartaDiv.onclick = () => {
                            document.querySelector(".input-red").value = i;
                            document.querySelector(".input-brsed").value = j;
                            document.querySelector(".input-cena").value = 500;
                        };
                        
                        kartaDiv.style.backgroundColor = "green"; 
                    }
                }
            }

        }


        onClickKupi = async () =>{
            let red= document.querySelector(".input-red").value;
            let sediste= document.querySelector(".input-brsed").value;
            let sifra= document.querySelector(".input-sifra").value;
        
            
            const result= await fetch("https://localhost:7080/Ispit/KupiKartu/"+ sifra + "/" + red + "/" + sediste, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
            }).then((response)=>response.text());

            
            console.log(result);

            //window.location.reload();

            const selektovanaProjekcija = this.projekcije.find(
                p => p.sifra == document.querySelector(".input-sifra").value
            );
            const karteDiv = document.querySelector(".karteDiv");
            this.drawKarte(selektovanaProjekcija, karteDiv);
        }

    

}