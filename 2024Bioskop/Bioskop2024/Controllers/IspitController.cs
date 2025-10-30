using System.Reflection.Metadata.Ecma335;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class IspitController : ControllerBase
{
    public IspitContext Context { get; set; }

    public IspitController(IspitContext context)
    {
        Context = context;
    }

    [HttpGet("VratiProjekcije")]
    public async Task<ActionResult> VratiProjekcije()
    {
        try{
            var projekcija = await Context.Projekcije.ToListAsync();
            return Ok(projekcija);
        }
        catch (Exception ex){
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("VratiKarte/{sifra}")]
    public async Task<ActionResult> VratiKarte(int sifra) 
    {
        try{
            var projekcija = await Context.Karte
                                    .Where(k=>k.Projekcija!.Sifra==sifra)
                                    .ToListAsync();
            return Ok(projekcija);

        }
        catch (Exception ex){
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("KupiKartu/{sifra}/{red}/{sediste}")]
    public async Task<ActionResult> KupiKartu(uint sifra, uint red, uint sediste)
    {
        try
        {
            var projekcija = await Context.Projekcije.FirstOrDefaultAsync(p => p.Sifra == sifra);

            if (projekcija == null)
            {
                return NotFound("Projekcija ne postoji");
            }
            if (red > projekcija.BrojReda || sediste > projekcija.BrojSedistaURedu)
            {
                return NotFound("Broj reda ili sedista je van opsega sale.");
            }

            var karta = new Karta
            {
                Projekcija = projekcija,
                Red = red,
                Sediste = sediste
            };

            await Context.Karte.AddAsync(karta);
            await Context.SaveChangesAsync();

            return Ok("Karta je kupljena");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    
}
