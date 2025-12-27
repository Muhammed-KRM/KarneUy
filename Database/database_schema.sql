-- Database Schema for Akademik Analiz ve Optik Değerlendirme Sistemi (Phase 1)

-- 1. Definitions
CREATE TABLE Lessons (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Topics (
    Id INT PRIMARY KEY IDENTITY(1,1),
    LessonId INT NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    ParentId INT NULL,
    CONSTRAINT FK_Topics_Lessons FOREIGN KEY (LessonId) REFERENCES Lessons(Id),
    CONSTRAINT FK_Topics_Parent FOREIGN KEY (ParentId) REFERENCES Topics(Id)
);

-- 2. Exams
CREATE TABLE ExamHeaders (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ExamName NVARCHAR(200) NOT NULL,
    ExamDate DATETIME DEFAULT GETDATE(),
    BookletType CHAR(1) NOT NULL -- 'A', 'B' etc.
);

CREATE TABLE ExamQuestions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ExamId INT NOT NULL,
    QuestionNo INT NOT NULL,
    CorrectAnswer CHAR(1) NOT NULL, -- 'A', 'B', 'C', 'D', 'E'
    LessonId INT NOT NULL,
    TopicId INT NOT NULL,
    CONSTRAINT FK_ExamQuestions_Exams FOREIGN KEY (ExamId) REFERENCES ExamHeaders(Id),
    CONSTRAINT FK_ExamQuestions_Lessons FOREIGN KEY (LessonId) REFERENCES Lessons(Id),
    CONSTRAINT FK_ExamQuestions_Topics FOREIGN KEY (TopicId) REFERENCES Topics(Id)
);

-- 3. Operations
CREATE TABLE RawDataUploads (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UploadDate DATETIME DEFAULT GETDATE(),
    FileName NVARCHAR(255),
    RawContent NVARCHAR(MAX) -- Full content or summary
);

CREATE TABLE Students (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StudentNumber NVARCHAR(50) UNIQUE NOT NULL,
    FullName NVARCHAR(100)
);

CREATE TABLE StudentExamResults (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StudentId INT NOT NULL,
    ExamId INT NOT NULL,
    TotalCorrect INT DEFAULT 0,
    TotalWrong INT DEFAULT 0,
    TotalEmpty INT DEFAULT 0,
    NetScore DECIMAL(5,2),
    Score DECIMAL(5,2),
    ProcessedDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_StudentResults_Students FOREIGN KEY (StudentId) REFERENCES Students(Id),
    CONSTRAINT FK_StudentResults_Exams FOREIGN KEY (ExamId) REFERENCES ExamHeaders(Id)
);

CREATE TABLE StudentAnswers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StudentExamResultId INT NOT NULL,
    QuestionNo INT NOT NULL,
    GivenAnswer CHAR(1), -- 'A', 'B' etc. or NULL for empty
    IsCorrect BIT DEFAULT 0,
    CONSTRAINT FK_StudentAnswers_Results FOREIGN KEY (StudentExamResultId) REFERENCES StudentExamResults(Id)
);

-- Indexes for performance
CREATE INDEX IX_ExamQuestions_ExamId ON ExamQuestions(ExamId);
CREATE INDEX IX_StudentResults_StudentId ON StudentExamResults(StudentId);
CREATE INDEX IX_StudentResults_ExamId ON StudentExamResults(ExamId);
