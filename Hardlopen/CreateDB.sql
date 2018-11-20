CREATE TABLE [Gebruiker] (
  [ID] int,
  [Naam] NCHAR(30),
  [Wachtwoord] NCHAR(50),
  [Email] NCHAR(30),
  [Geslacht] NCHAR(5),
  [Lengte] FLOAT,
  [Gewicht] decimal(6,2),
  [GemiddeldeTijd(s)] int,
  PRIMARY KEY ([ID])
);

CREATE TABLE [Loopmoment] (
  [ID] int,
  [Gebruiker] int,
  [Playlist] int,
  [Route] int,
  [Tijd] int,
  [Datum] date,
  [Afstand] int,
  [GemiddeldeSnelheid] decimal(7,3),
  PRIMARY KEY ([ID])
);

CREATE INDEX [FK] ON  [Loopmoment] ([Gebruiker], [Playlist], [Route]);

CREATE TABLE [Liedje] (
  [ID] int,
  [Titel] NCHAR(20),
  [Artiest] NCHAR(20),
  [Tijdsduur(s)] int,
  PRIMARY KEY ([ID])
);

CREATE TABLE [Route] (
  [Nummer] int,
  [Afstand(m)] int,
  [Tijdsduur(s)] int,
  PRIMARY KEY ([Nummer])
);

CREATE TABLE [Gebruiker-Playlist] (
  [GebruikerID] int,
  [PlaylistID] int
);

CREATE INDEX [FK] ON  [Gebruiker-Playlist] ([GebruikerID], [PlaylistID]);

CREATE TABLE [Playlist] (
  [ID] int,
  [Naam] NCHAR(20),
  PRIMARY KEY ([ID])
);

CREATE TABLE [Playlist-Liedje] (
  [PlaylistID] int,
  [LiedjeID] int
);

CREATE INDEX [FK] ON  [Playlist-Liedje] ([PlaylistID], [LiedjeID]);

