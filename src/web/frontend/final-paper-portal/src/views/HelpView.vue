<template>
  <div class="page-content">
    <h1 class="title">Često postavljena pitanja</h1>
    <Accordion class="">
      <AccordionTab header="Kako se odabire mentor za završni rad putem ove aplikacije?">
        <p>
          Odabir mentora za završni rad putem ove aplikacije obično se odvija na sljedeći način:
          student se prijavljuje na aplikaciju i ima pristup popisu dostupnih mentora. Student može
          pregledati profile mentora, njihova područja stručnosti i dostupnost. Nakon odabira
          mentora, student može poslati zahtjev za mentorstvo.
        </p>
      </AccordionTab>
      <AccordionTab header="Kako mogu prijaviti temu za završni rad putem aplikacije?">
        <p>
          Da biste prijavili temu za završni rad putem aplikacije, trebate pristupiti odjeljku za
          prijavu tema. U tom odjeljku možete pregledati dostupne teme za završni rad. Kada
          pronađete temu koja vam odgovara, možete je odabrati i poslati svoju prijavu za tu temu.
        </p>
      </AccordionTab>
      <AccordionTab header="Kako mogu pregledati sve dostupne teme za završni rad u aplikaciji?">
        <p>
          Da biste pregledali sve dostupne teme za završni rad u aplikaciji, trebate pristupiti
          odjeljku s temama. U tom odjeljku bit će vam prikazan popis svih dostupnih tema za završni
          rad. Možete pregledavati teme i saznati više informacija o svakoj od njih prije nego što
          odaberete odgovarajuću temu.
        </p>
      </AccordionTab>
      <AccordionTab header="Kako mogu komunicirati s mentorom putem ove aplikacije?">
        <p>
          Komunikacija s mentorom putem ove aplikacije obično se ostvaruje putem integrirane
          komunikacijske platforme. Na raspolaganju vam mogu biti opcije kao što su razmjena poruka,
          sastanci putem video poziva ili mogućnost postavljanja pitanja mentoru. Detalji o načinu
          komunikacije bit će vam dostupni unutar aplikacije.
        </p>
      </AccordionTab>
      <AccordionTab
        header="Hoće li sve aktivnosti vezane uz završne radove biti obavljene putem ove jedne aplikacije?"
      >
        <p>
          Da, ova aplikacija je namijenjena za obavljanje svih aktivnosti vezanih uz završne radove.
          Možete pristupiti temama, komunicirati s mentorom, pratiti status svog završnog rada i
          obavljati druge relevantne zadatke sve unutar ove jedne aplikacije.
        </p>
      </AccordionTab>
      <AccordionTab header="Kako će studenti, mentori i administratori pristupati aplikaciji?">
        <p>
          Studenti će se prijaviti sa svojim korisničkim računima koje su stvorili prilikom
          registracije. Nakon prijave, imat će pristup funkcionalnostima kao što su pregled tema,
          slanje zahtjeva za mentorstvo, komunikacija s mentorom i praćenje statusa završnog rada.
          Mentori će također imati svoje korisničke račune te će se prijavljivati na isti način kao
          i studenti. Kroz svoj korisnički račun, mentori će moći pregledavati zahtjeve za
          mentorstvo, komunicirati sa studentima te pružati smjernice i podršku tijekom procesa
          završnog rada. Administratori će imati posebne korisničke račune s proširenim ovlastima.
          Oni će biti odgovorni za upravljanje aplikacijom, dodavanje i ažuriranje tema, upravljanje
          korisničkim računima te rješavanje problema ili zahtjeva vezanih za aplikaciju.
        </p>
      </AccordionTab>
      <AccordionTab header="Koje su mogućnosti studenata u ovoj aplikaciji?">
        <p>
          Studenti će imati razne mogućnosti unutar ove aplikacije. Nekoliko njih su: Pregled
          dostupnih tema za završni rad. Slanje zahtjeva za mentorstvo odabranom mentoru.
          Komunikacija s mentorom putem integrirane komunikacijske platforme. Praćenje statusa svog
          završnog rada. Pregledavanje resursa i materijala vezanih uz završni rad. Slanje prijava
          završnog rada putem aplikacije. Pregled povratnih informacija i ocjena od strane mentora.
        </p>
      </AccordionTab>
      <AccordionTab header="Kako će se odvijati proces prijave završnog rada putem aplikacije?">
        <p>
          Proces prijave završnog rada putem aplikacije uključivat će sljedeće korake: Student će
          pristupiti odjeljku s prijavama završnog rada. U odjeljku će moći pregledati raspoložive
          teme i odabrati odgovarajuću. Nakon odabira teme, student će ispuniti prijavni obrazac sa
          relevantnim podacima. Potrebno je priložiti sve potrebne dokumente ili materijale. Kada je
          prijava spremna, student će je poslati na pregled i odobrenje.
        </p>
      </AccordionTab>
      <AccordionTab header="Kako mogu provjeriti status svog završnog rada u aplikaciji?">
        <p>
          Da biste provjerili status svog završnog rada u aplikaciji, možete pristupiti svojem
          korisničkom računu i otići na odjeljak koji prikazuje informacije o završnom radu. U tom
          odjeljku bit će vam prikazan trenutni status vašeg rada, kao što su prihvaćanje prijave,
          dodjela mentora, datum predaje itd. Također, možete primati obavijesti o bilo kojim
          promjenama ili ažuriranjima vezanim uz vaš završni rad.
        </p>
      </AccordionTab>
    </Accordion>
  </div>
</template>

<script>
export default {
  name: 'HelpView'
}
</script>

<style scoped>
.page-content {
  height: 100%;
  margin-left: 15vw;
  margin-right: 15vw;
  margin-top: 5em;
}
</style>
