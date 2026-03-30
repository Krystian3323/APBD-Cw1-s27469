\# RentMePls - System Wypożyczalni Sprzętu Akademickiego



\## Przegląd projektu

Aplikacja konsolowa w języku C# przeznaczona do zarządzania uczelnianą wypożyczalnią sprzętu. System umożliwia rejestrację zasobów, zarządzanie użytkownikami (studentami i pracownikami), obsługę procesów wypożyczeń/zwrotów oraz automatyczne naliczanie kar za nieterminowość.



\## Struktura i Decyzje Projektowe



\### 1. Model Domenowy (Enkapsulacja i Dziedziczenie)

\* \*\*Klasa abstrakcyjna `Hardware`\*\*: Stanowi bazę dla wszystkich urządzeń. Zawiera wspólne właściwości, takie jak `Id` (Guid), `Name`, `Brand` oraz `SerialNumber`. Jest abstrakcyjna, ponieważ w systemie nie istnieje "generyczny" sprzęt – zawsze musi to być konkretny typ.

\* \*\*Klasy specjalistyczne\*\*: `Laptop`, `Camera` oraz `Printer` dziedziczą po `Hardware`, dodając specyficzne pola (np. RAM dla laptopów, rozdzielczość dla kamer).

\* \*\*Klasa `User`\*\*: Zamiast tworzyć wiele klas dla typów użytkowników, zastosowano właściwość `Role`. Pozwala to na uniknięcie sztucznego mnożenia klas przy jednoczesnym zachowaniu logiki limitów (2 dla studentów, 5 dla pracowników).



\### 2. Logika i Odpowiedzialność (Kohezja i Sprzężenie)

\* \*\*Klasa `Rental` (Wypożyczenie)\*\*: Odpowiada za logikę pojedynczego zdarzenia wypożyczenia. "Wie", jak obliczyć własną karę (`CalculateFine`) i zarządzać swoimi datami. Zapewnia to wysoką \*\*kohezję\*\* (spójność) – logika kary jest tam, gdzie dane o terminach.

\* \*\*Klasa `RentalService`\*\*: Pełni rolę "mózgu" systemu. Zarządza kolekcjami obiektów i egzekwuje reguły biznesowe, takie jak sprawdzanie dostępności sprzętu przed wypożyczeniem czy weryfikacja limitów użytkownika.

\* \*\*Niskie Sprzężenie (Low Coupling)\*\*: Dzięki operowaniu na klasie bazowej `Hardware` wewnątrz obiektu `Rental` (zamiast na konkretnych typach jak `Laptop`), system jest elastyczny. Możemy dodać nowy typ sprzętu bez modyfikacji klasy `Rental`.



\### 3. Implementacja Reguł Biznesowych

\* \*\*Unikalne ID\*\*: Sprzęt wykorzystuje `Guid` dla zapewnienia globalnej unikalności, natomiast użytkownicy korzystają z automatycznie inkrementowanego `int`.

\* \*\*Walidacja\*\*: Konstruktory zawierają mechanizmy obronne (rzucanie wyjątków `ArgumentException`), co zapobiega tworzeniu niekompletnych lub błędnych obiektów.

\* \*\*Kary\*\*: Naliczane automatycznie w wysokości 10 PLN za każdy dzień po przekroczeniu 7-dniowego terminu.



\## Instrukcja Uruchomienia

1\. Upewnij się, że masz zainstalowane .NET SDK.

2\. Sklonuj repozytorium.

3\. W folderze projektu wykonaj polecenie:

&#x20;  

&#x20;  dotnet run

