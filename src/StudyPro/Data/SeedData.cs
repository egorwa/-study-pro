using Microsoft.EntityFrameworkCore;
using StudyPro.Models;

namespace StudyPro.Data;

public static class SeedData
{
    public static async Task InitializeAsync(StudyProContext db)
    {
        // Only seed if no tasks exist yet so restarts don't create duplicates
        if (await db.StudyTasks.AnyAsync())
        {
            return;
        }

        var now = DateTime.UtcNow;

        var initialTasks = new List<StudyTask>
        {
            new()
            {
                Title = "Review Entity Framework Core migrations",
                Subject = "ASP.NET Core",
                Notes = "Practice creating migrations, updating SQLite schema, and checking generated tables.",
                DueDate = now.AddDays(2),
                IsCompleted = false,
                CreatedAt = now
            },
            new()
            {
                Title = "Summarize Chapter 4 lecture notes",
                Subject = "Database Systems",
                Notes = "Focus on normalization rules, indexing trade-offs, and relational keys.",
                DueDate = now.AddDays(4),
                IsCompleted = false,
                CreatedAt = now.AddMinutes(-15)
            },
            new()
            {
                Title = "Complete past paper practice set",
                Subject = "Algorithms",
                Notes = "Time yourself on recursion, sorting complexity, and graph traversal problems.",
                DueDate = now.AddDays(7),
                IsCompleted = false,
                CreatedAt = now.AddMinutes(-30)
            },
            new()
            {
                Title = "Set up Study Pro solution and DbContext",
                Subject = "Capstone Project",
                Notes = "Configured StudyProContext, SQLite connection string, and initial controller.",
                DueDate = now.AddDays(-1),
                IsCompleted = true,
                CreatedAt = now.AddHours(-2)
            },
           
            new()
            {
                Title = "Solve multivariable gradient and chain rule problems",
                Subject = "Calculus III",
                Notes = "Complete problem set 6 covering partial derivatives and directional gradients.",
                DueDate = now.AddDays(3),
                IsCompleted = false,
                CreatedAt = now.AddHours(-3)
            },
            new()
            {
                Title = "Practice eigenvalues and matrix diagonalization",
                Subject = "Linear Algebra",
                Notes = "Work through characteristic polynomials and orthonormal basis examples.",
                DueDate = now.AddDays(5),
                IsCompleted = false,
                CreatedAt = now.AddHours(-4)
            },
            new()
            {
                Title = "Review TCP congestion control and subnetting",
                Subject = "Computer Networks",
                Notes = "Draw sequence diagrams for three-way handshake and CIDR routing tables.",
                DueDate = now.AddDays(6),
                IsCompleted = false,
                CreatedAt = now.AddHours(-5)
            },
            new()
            {
                Title = "Outline literature review and research methodology",
                Subject = "Research Methods",
                Notes = "Summarize 5 core academic sources and define study inclusion criteria.",
                DueDate = now.AddDays(8),
                IsCompleted = false,
                CreatedAt = now.AddHours(-6)
            },
            new()
            {
                Title = "Build responsive layout and form validation lab",
                Subject = "Web Development",
                Notes = "Test semantic HTML structure, CSS grid breakpoints, and client validation.",
                DueDate = now.AddDays(4),
                IsCompleted = false,
                CreatedAt = now.AddHours(-7)
            },
            new()
            {
                Title = "Complete process scheduling and deadlock exercises",
                Subject = "Operating Systems",
                Notes = "Compare round-robin vs. priority scheduling and Banker's algorithm traces.",
                DueDate = now.AddDays(9),
                IsCompleted = false,
                CreatedAt = now.AddHours(-8)
            },
            new()
            {
                Title = "Review SOLID principles and refactoring patterns",
                Subject = "Software Engineering",
                Notes = "Refactor tightly coupled classes using dependency injection and interfaces.",
                DueDate = now.AddDays(5),
                IsCompleted = true,
                CreatedAt = now.AddHours(-9)
            },
            new()
            {
                Title = "Analyze hypothesis testing and p-value distributions",
                Subject = "Probability & Statistics",
                Notes = "Run t-test and chi-square examples and check assumptions on sample data.",
                DueDate = now.AddDays(6),
                IsCompleted = false,
                CreatedAt = now.AddHours(-10)
            },
            new()
            {
                Title = "Audit authentication flow and input sanitization",
                Subject = "Information Security",
                Notes = "Review password hashing, session cookies, and XSS/SQL injection defenses.",
                DueDate = now.AddDays(10),
                IsCompleted = false,
                CreatedAt = now.AddHours(-11)
            },
            new()
            {
                Title = "Prepare sprint demo and user story acceptance criteria",
                Subject = "Agile Project Management",
                Notes = "Update backlog priorities, story points, and sprint burndown notes.",
                DueDate = now.AddDays(2),
                IsCompleted = false,
                CreatedAt = now.AddHours(-12)
            }
        };

        db.StudyTasks.AddRange(initialTasks);
        await db.SaveChangesAsync();
    }
}