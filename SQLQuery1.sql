create database TourBookingDB
use TourBookingDB
-- =========================
-- ENUM Mappings (use VARCHAR or INT)
-- UserRole: 'Customer', 'Consultant'
-- ParticipantType: 'Player', 'Family', 'Staff'
-- TourStatus: 'Save', 'Submit', 'Approved', 'OnHold', 'Cancelled', 'Closed'
-- BookingStatus: 'SaveAsDraft', 'Submit'
-- =========================

CREATE TABLE AuthUser (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Gender VARCHAR(10),
    DOB DATE,
    Role VARCHAR(20) CHECK (Role IN ('Customer','Consultant')),
    UserName VARCHAR(100) UNIQUE NOT NULL,
    Password VARCHAR(200) NOT NULL,
    EmailId VARCHAR(150),
    TelephoneNo VARCHAR(20)
);

CREATE TABLE Destination (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name VARCHAR(150) NOT NULL,
    City VARCHAR(100) NOT NULL
);

CREATE TABLE Tour (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TourName VARCHAR(150) NOT NULL,
    TourDescription VARCHAR(500),
    DestinationId UNIQUEIDENTIFIER NOT NULL,
    NoOfNights VARCHAR(50),
    DepartureDate DATE,
    ArrivalDate DATE,
    Customer VARCHAR(150),
    Status VARCHAR(20) CHECK (Status IN ('Save','Submit','Approved','OnHold','Cancelled','Closed')),
    Consultant CHAR(1),
    TermsId INT,
    CONSTRAINT FK_Tour_Destination FOREIGN KEY (DestinationId) REFERENCES Destination(Id)
);

CREATE TABLE TermsAndConditions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TourId UNIQUEIDENTIFIER NOT NULL,
    Terms VARCHAR(500),
    CONSTRAINT FK_Terms_Tour FOREIGN KEY (TourId) REFERENCES Tour(Id)
);

CREATE TABLE TourBookingForm (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TourId UNIQUEIDENTIFIER NOT NULL,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Gender CHAR(1),
    DOB DATE,
    Citizenship VARCHAR(100),
    PassportNumber VARCHAR(50),
    IssueDate DATE,
    ExpiryDate DATE,
    PlaceOfBirth VARCHAR(100),
    LeadPassenger BIT,
    ParticipantType VARCHAR(20) CHECK (ParticipantType IN ('Player','Family','Staff')),
    Status VARCHAR(20) CHECK (Status IN ('SaveAsDraft','Submit')),
    CONSTRAINT FK_TourBooking_Tour FOREIGN KEY (TourId) REFERENCES Tour(Id)
);

CREATE TABLE ParticipantInformation (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    LeadId UNIQUEIDENTIFIER NOT NULL,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Gender CHAR(1),
    Citizenship VARCHAR(100),
    PassportNumber VARCHAR(50),
    IssueDate DATE,
    ExpiryDate DATE,
    PlaceOfBirth VARCHAR(100),
    CONSTRAINT FK_Participant_Booking FOREIGN KEY (LeadId) REFERENCES TourBookingForm(Id)
);
