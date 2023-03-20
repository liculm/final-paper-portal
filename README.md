VELEUČILIŠTE U RIJECI

POSLOVNI ODJEL RIJEKA

STRUČNI STUDIJ INFORMATIKA

**SUSTAV ZAVRŠNIH RADOVA**

PROJEKTNA DOKUMENTACIJA

Rijeka, 2023.

VELEUČILIŠTE U RIJECI

POSLOVNI ODJEL RIJEKA

SPECIJALISTIČKI STUDIJ INFORMACIJSKE TEHNOLOGIJE U POSLOVNIM SUSTAVIMA

**SUSTAV ZAVRŠNIH RADOVA**

PROJEKTNA DOKUMENTACIJA

Kolegij: Alati razvoja informacijskih sustava

Mentori: Marin Kaluža

Studenti: Paolo Baldaš, Emil Krulčić, Masimo Licul

Rijeka, 2023.

**Sadržaj**

# Sadržaj

[1.Opis sustava Webshop 4](#_Toc129867939)

[2. Specifikacija zahtjeva 5](#_Toc129867940)

[2.1 Interview sa korisnikom 5](#_Toc129867941)

[2.2 Korisničke priče 6](#_Toc129867942)

[2.2. Ostale specifikacije zahtjeva 7](#_Toc129867943)

1.
# Opis sustava Webshop

Osnovna funkcija sustava završnih radova bi bila pojednostavljen odabir mentora završnog rada, prijave navedene teme, prikaz svih dostupnih tema, komunikacija na relaciji student-mentor. Zamišljeno je da se radi o jednoj aplikaciji, a ne zasebno više njih, što znači kako će se sve aktivnosti poput prijave teme, mentora, zaduženja u knjižnici, primanje ocjene, komentari i sl. Odvijati sve putem iste – jedne aplikacije. To znači kako će studenti, mentori te administratori pristupati istoj aplikaciji, dok će svaki na temelju svoje razine (student, mentor, administrator) imati svoje mogućnosti.

Student će imati svoje zasebno sučeljem, a prilikom odabira mentora i teme, zamišljeno je kako se to odvija „in one go", odnosno u koracima, kako bi se izbjegle svakakvi mogući nesporazumi i krive radnje kod studenta. Prilikom prijave zvršnog rada, studentu bi se na sučelju pokakazali koraci prijave završnog rada. Npr. Korak 1 – Odaberi mentora (ukoliko je slobodan), korak 2 – odaberi temu i kolegij – Korak 3 – sažetak i slanje zahtjeva. Nakon toga, sučelje sa koracima se miče, te se studenta navodi na klasično sučelje. Ondje će moći pregledavati status završnog rada (je li tema prihvaćena ili ne), vršiti komunikaciju sa mentorom, verzije završnog rada, komentari, datum i vrijeme izlaganja obrane i broj dvoran i sl.

Kao mentoru, zamišljeno je da se sučelje sastoji sa svim funkcijama kojemu su istome potrebne, pa bi tako mentor mogao pregledavati sve studente koje ima pod mentorstvom, pregledavati sve zahtjeve za mentorstvo (prihvaćanje ili odbijanje), vršiti komunikaciju sa svakim zasebnom, te naposljetku označavanje završnog rada pojedinog studenta kao npr, „Spreman za obranu" koje iza sebe ima određene aktivnosti.

Administrator bi bio voditelj cijelog sustava, pa bi tako mogao dodavati nove studente, nove mentore, pregledavati sve završne radove, odnosno vršiti sve moguće izmjene kako za studente pa tako i mentore.

# 2. Specifikacija zahtjeva

U ovom poglavlju definirati će se specifikacije zahtjeva koje su dobivene nakon razgovora sa klijentom.

## 2.1 Interview sa korisnikom

Klijent želi da je aplikacija jednostavna za korištenje, kako ne bi uzrokovala komplikacije pri uvođenju u rad. Aplikacija mora biti jednostavna za korištenje, naročito od strane studenta kako bi se istima sve olakšalo.

Prije svakog korištenja korisnik se treba ulogirati kako bi znali koji pristup ima.

Klijent zahtjeva mogućnost prijave/odabira teme, kolegija i mentora kod strane studenta. Nužno je da je moguće vršiti komunikaciju između studenta i mentora koja je velike važnost, što znači kako će student moći prenijeti svoju trenutnu verziju završnog rada a za koju će mu mentor napisiti komentare. Student bi trebao prilikom slanja zahtjeva moći pratiti status svog zahtjeva, odnosno je li tema i mentor prihvaćen ili ne.

Klijent zahtjeva kako mentora mora imati jednostavan pregled svojih studenata koji su pod njegovim mentorstvom. Mora moći pregledavati verzije radova koji mu određeni student prikaže te mora imati mogućnost ostavljanja komentara za svaki, odnosno mora moći vodi kvalitetnu komunikaciju sa svakim. Kada je završni rad spreman, mora moći „zaključati" rad, odnosno označiti rad kao spremnim te slanje studenta na obranu.

Administrator će dodavati nove studente i mentore, definirati datume obrane i moguće dvorane za istu, komisiju,..odnosno mora moći izmjenjivati i dodavati sve što se tiće studenta i mentora te cjeloukupnog sustava.

## 2.2 Korisničke priče

| Redni broj | Uloga | Zahtjev/funkcionalnost/aktivnost | Opis funkcionalnosti |
| --- | --- | --- | --- |
| 1. | Student/Mentor/Administrator | Prijava u sustav | Mogućnost prijave u sustav |
| 2. | Student | Zahtjev za prijavu mentora i teme | Student ima mogućnost slanje zahtjeva za mentorstvo koje sa sobom donosi i temu završnog rada kao i kolegij za koji se izrađuje. |
| 3. | Student | Komunikacija sa mentorom | Student ima mogućnost komunikacije sa mentorom. |
| 4. | Student | Slanje nove verzije rada | Student ima mogućnost slanja nove verzije rada mentoru. |
| 5. | Student | Status završnog rada/obrane | Student ima mogućnost prikaza trenutnog status rada, kada, gdje i u koje vrijeme je obrana ako su za to zadovoljeni uvjeti. |
| 6. | Mentor | Prikaz i prihvaćanje/odbijanje zahtjeva za mentorstvo | Mentor ima mogućnost prikaza svih zahtjeva, te iste prihvaća/odbija. |
| 7. | Mentor | Komunikacija sa studentom | Mentor ima pregledavanja svih verzija koje mu student pošalje za koje ima mogućnost ostavljanja komentara. |
| 8. | Mentor | Slanje studenta na obranu | Mentor ima mogućnost slanja studenta na obranu rada ukoliko su za to uvjeti ispunjeni. |
| 9. | Administrator | Dodavanje/uređivanje kolegija, studenata, mentora, termina obrane, dvorana,... | Administrator ima punu kontrolu nad sustavom. |

## 2.2. Ostale specifikacije zahtjeva

| Redni broj | Zahtjev/funkcionalnost/aktivnost | Opis funkcionalnosti |
| --- | --- | --- |
| 1. | Jednostavnost aplikacije | Aplikacija mora biti jednostavna za korištenje, naročito od strane studenta. Za korištenje aplikacije nije potrebno nikakvo predznanje. |
| 2. | Jednostavno grafičko sučelje (klasičan I moderan webshop) | Korisnik želi da sučelje web shopa bude klasično te moderno u skladu sa današnjim trendovima. |
| 3. | Ograničenja unosa | Ograničenja unosa – nije moguće proslijediti na sljedeći korak ukoliko sva polja podataka nisu unesena. |
| 4. | Brz I fluidan rad | Aplikacija mora biti brza i fluidna. Ne smije se rušiti te ograničavati rad korisnika. |