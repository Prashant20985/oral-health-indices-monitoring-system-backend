using App.Domain.Models.CreditSchema;
using App.Domain.Models.OralHealthExamination;
using App.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Contexts;
/// <summary>
///  Represents the database context for the OralEhr application.
///  Inherits from the IdentityDbContext class to provide user and role management functionality.
/// Contains the database tables for the User, RefreshToken, ApplicationRole, and ApplicationUserRole entities.
/// </summary>
public class OralEhrContext : IdentityDbContext<
    ApplicationUser, ApplicationRole, string,
    IdentityUserClaim<string>, ApplicationUserRole,
    IdentityUserLogin<string>, IdentityRoleClaim<string>,
    IdentityUserToken<string>>
{
    /// <summary>
    /// Parameterless constructor provided for the framework to create an instance of the UserContext class.
    /// </summary>
    public OralEhrContext()
    {

    }

    /// <summary>
    /// Constructor that accepts DbContextOptions<UserContext> as a parameter and passes it to the base class constructor.
    /// Allows the configuration options for the UserContext to be provided externally.
    /// </summary>
    /// <param name="options">The options for configuring the UserContext.</param>
    public OralEhrContext(DbContextOptions<OralEhrContext> options) : base(options)
    {

    }

    /// <summary>
    /// Represents the database table for the User entities.
    /// </summary>
    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    /// <summary>
    /// Represents the database table for the RefreshToken entities.
    /// </summary>
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    /// <summary>
    /// Represents the database table for the ApplicationRole entities.
    /// </summary>
    public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }

    /// <summary>
    /// Represents the database table for the ApplicationUserRole entities.
    /// </summary>
    public virtual DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

    /// <summary>
    /// Represents the database table for the Group entities.
    /// </summary>
    public virtual DbSet<Group> Groups { get; set; }

    /// <summary>
    /// Represents the database table for the StudentGroup entities.
    /// </summary>
    public virtual DbSet<StudentGroup> StudentGroups { get; set; }

    /// <summary>
    /// Represents the database table for the UserRequest entities.
    /// </summary>
    public virtual DbSet<UserRequest> UserRequests { get; set; }

    /// <summary>
    /// Represents the database table for the Supervise entities.
    /// </summary>
    public virtual DbSet<Supervise> Supervises { get; set; }

    /// <summary>
    /// Gets or sets the set of patients.
    /// </summary>
    public virtual DbSet<Patient> Patients { get; set; }

    /// <summary>
    /// Gets or sets the set of research groups.
    /// </summary>
    public virtual DbSet<ResearchGroup> ResearchGroups { get; set; }

    /// <summary>
    /// Gets or sets the set of patient examination cards.
    /// </summary>
    public virtual DbSet<PatientExaminationCard> PatientExaminationCards { get; set; }

    /// <summary>
    /// Gets or sets the set of risk factor assessments.
    /// </summary>
    public virtual DbSet<RiskFactorAssessment> RiskFactorAssessments { get; set; }

    /// <summary>
    /// Gets or sets the set of BEWE assessments.
    /// </summary>
    public virtual DbSet<Bewe> Bewes { get; set; }

    /// <summary>
    /// Gets or sets the set of DMFT_DMFS assessments.
    /// </summary>
    public virtual DbSet<DMFT_DMFS> DMFT_DMFSs { get; set; }

    /// <summary>
    /// Gets or sets the set of API bleeding assessments.
    /// </summary>
    public virtual DbSet<API> APIs { get; set; }

    /// <summary>
    /// Gets or sets the set of bleeding assessments.
    /// </summary>
    public virtual DbSet<Bleeding> Bleedings { get; set; }

    /// <summary>
    /// Gets or sets the set of patient examination results.
    /// </summary>
    public virtual DbSet<PatientExaminationResult> PatientExaminationResults { get; set; }

    /// <summary>
    /// Gets or sets the set of exams.
    /// </summary>
    public virtual DbSet<Exam> Exams { get; set; }

    /// <summary>
    /// Gets or sets the set of practice patients.
    /// </summary>
    public virtual DbSet<PracticePatient> PracticePatients { get; set; }

    /// <summary>
    /// Gets or sets the set of practice patient examination cards.
    /// </summary>
    public virtual DbSet<PracticePatientExaminationCard> PracticePatientExaminationCards { get; set; }

    /// <summary>
    /// Gets or sets the set of practice patient examination results.
    /// </summary>
    public virtual DbSet<PracticePatientExaminationResult> PracticePatientExaminationResults { get; set; }

    /// <summary>
    /// Gets or sets the set of practice risk factor assessments.
    /// </summary>
    public virtual DbSet<PracticeRiskFactorAssessment> PracticeRiskFactorAssessments { get; set; }

    /// <summary>
    /// Gets or sets the set of practice BEWE assessments.
    /// </summary>
    public virtual DbSet<PracticeBewe> PracticeBewes { get; set; }

    /// <summary>
    /// Gets or sets the set of practice DMFT_DMFS assessments.
    /// </summary>
    public virtual DbSet<PracticeDMFT_DMFS> PracticeDMFT_DMFSs { get; set; }

    /// <summary>
    /// Gets or sets the set of practice API bleeding assessments.
    /// </summary>
    public virtual DbSet<PracticeAPI> PracticeAPIs { get; set; }

    /// <summary>
    /// Gets or sets the set of practice bleeding assessments.
    /// </summary>
    public virtual DbSet<PracticeBleeding> PracticeBleedings { get; set; }

    /// <summary>
    /// Overrides the OnModelCreating method from the base class to provide custom model configuration.
    /// </summary>
    /// <param name="modelBuilder">The model builder instance to configure the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationRole>().ToTable(nameof(ApplicationRole), "user");

        modelBuilder.Entity<ApplicationUserRole>().ToTable(nameof(ApplicationUserRole), "user");

        modelBuilder.Entity<ApplicationUser>().ToTable(nameof(ApplicationUser), "user");

        modelBuilder.Entity<RefreshToken>().ToTable(nameof(RefreshToken), "user");

        modelBuilder.Entity<Group>().ToTable(nameof(Group), "user");

        modelBuilder.Entity<StudentGroup>().ToTable(nameof(StudentGroup), "user");

        modelBuilder.Entity<UserRequest>().ToTable(nameof(UserRequests), "user");

        modelBuilder.Entity<Supervise>().ToTable(nameof(Supervise), "user");

        modelBuilder.Entity<Patient>().ToTable(nameof(Patient), "oralHealthExamination");

        modelBuilder.Entity<ResearchGroup>().ToTable(nameof(ResearchGroup), "oralHealthExamination");

        modelBuilder.Entity<PatientExaminationCard>().ToTable(nameof(PatientExaminationCard), "oralHealthExamination");

        modelBuilder.Entity<RiskFactorAssessment>().ToTable(nameof(RiskFactorAssessment), "oralHealthExamination");

        modelBuilder.Entity<Bewe>().ToTable(nameof(Bewe), "oralHealthExamination");

        modelBuilder.Entity<DMFT_DMFS>().ToTable(nameof(DMFT_DMFS), "oralHealthExamination");

        modelBuilder.Entity<API>().ToTable(nameof(API), "oralHealthExamination");

        modelBuilder.Entity<Bleeding>().ToTable(nameof(Bleeding), "oralHealthExamination");

        modelBuilder.Entity<PatientExaminationResult>().ToTable(nameof(PatientExaminationResult), "oralHealthExamination");

        modelBuilder.Entity<Exam>().ToTable(nameof(Exam), "credit");

        modelBuilder.Entity<PracticePatient>().ToTable(nameof(PracticePatient), "credit");

        modelBuilder.Entity<PracticePatientExaminationCard>().ToTable(nameof(PracticePatientExaminationCard), "credit");

        modelBuilder.Entity<PracticePatientExaminationResult>().ToTable(nameof(PracticePatientExaminationResult), "credit");

        modelBuilder.Entity<PracticeRiskFactorAssessment>().ToTable(nameof(PracticeRiskFactorAssessment), "credit");

        modelBuilder.Entity<PracticeBewe>().ToTable(nameof(PracticeBewe), "credit");

        modelBuilder.Entity<PracticeDMFT_DMFS>().ToTable(nameof(PracticeDMFT_DMFS), "credit");

        modelBuilder.Entity<PracticeBleeding>().ToTable(nameof(PracticeBleeding), "credit");

        modelBuilder.Entity<PracticeAPI>().ToTable(nameof(PracticeAPI), "credit");

        // Ignores the IdentityUserClaim, IdentityRoleClaim, IdentityUserLogin, and IdentityUserToken entities.
        modelBuilder.Ignore<IdentityUserClaim<string>>();
        modelBuilder.Ignore<IdentityRoleClaim<string>>();
        modelBuilder.Ignore<IdentityUserLogin<string>>();
        modelBuilder.Ignore<IdentityUserToken<string>>();

        // Applies the entity configurations defined in the assembly containing the UserContext class.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OralEhrContext).Assembly);
    }
}
