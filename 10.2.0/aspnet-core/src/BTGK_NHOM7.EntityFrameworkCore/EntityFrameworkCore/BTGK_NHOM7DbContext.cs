using Abp.Zero.EntityFrameworkCore;
using BTGK_NHOM7.Authorization.Roles;
using BTGK_NHOM7.Authorization.Users;
using BTGK_NHOM7.Entities;
using BTGK_NHOM7.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using BTGK_NHOM7.ToeicExams;

namespace BTGK_NHOM7.EntityFrameworkCore;

public class BTGK_NHOM7DbContext : AbpZeroDbContext<Tenant, Role, User, BTGK_NHOM7DbContext>
{
    /* Define a DbSet for each entity of the application */
    public BTGK_NHOM7DbContext(DbContextOptions<BTGK_NHOM7DbContext> options)
        : base(options)
    {
    }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<QuestionGroup> QuestionGroups { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public virtual DbSet<ToeicExam> ToeicExams { get; set; }
    public virtual DbSet<ToeicPart> ToeicParts { get; set; }
    public virtual DbSet<ToeicPassage> ToeicPassages { get; set; }
    public virtual DbSet<ToeicQuestion> ToeicQuestions { get; set; }
    
}