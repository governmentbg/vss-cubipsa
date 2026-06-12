using System.Data.Entity;
using Legalacts.Model.Entities.Mapping;

namespace Legalacts.Model.Entities
{
    public partial class LegalactsContext : DbContext
    {
        static LegalactsContext()
        {
            Database.SetInitializer<LegalactsContext>(null);
        }

        public LegalactsContext()
            : base("Name=LegalactsContext")
        {
        }

        public DbSet<ActionLogType> ActionLogTypes { get; set; }
        public DbSet<ActKind> ActKinds { get; set; }
        public DbSet<Act> Acts { get; set; }
        public DbSet<AppealKind> AppealKinds { get; set; }
        public DbSet<CaseKind> CaseKinds { get; set; }
        public DbSet<ConnectedCase> ConnectedCases { get; set; }
        public DbSet<ConnectedKind> ConnectedKinds { get; set; }
        public DbSet<ConnectedType> ConnectedTypes { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<HigherCourt> HigherCourts { get; set; }
        public DbSet<IndocKind> IndocKinds { get; set; }
        public DbSet<Involvement> Involvements { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<ResultsOfAppeal> ResultsOfAppeals { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SendToDocumentKind> SendToDocumentKinds { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ActDocument> ActDocuments { get; set; }
        public DbSet<MotiveDocument> MotiveDocuments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<DeletedAct> DeletedActs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ActionLogTypeMap());
            modelBuilder.Configurations.Add(new ActKindMap());
            modelBuilder.Configurations.Add(new ActMap());
            modelBuilder.Configurations.Add(new AppealKindMap());
            modelBuilder.Configurations.Add(new CaseKindMap());
            modelBuilder.Configurations.Add(new ConnectedCasMap());
            modelBuilder.Configurations.Add(new ConnectedKindMap());
            modelBuilder.Configurations.Add(new ConnectedTypeMap());
            modelBuilder.Configurations.Add(new CourtMap());
            modelBuilder.Configurations.Add(new HigherCourtMap());
            modelBuilder.Configurations.Add(new IndocKindMap());
            modelBuilder.Configurations.Add(new InvolvementMap());
            modelBuilder.Configurations.Add(new LinkMap());
            modelBuilder.Configurations.Add(new LogMap());
            modelBuilder.Configurations.Add(new PermissionMap());
            modelBuilder.Configurations.Add(new ResultsOfAppealMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new SendToDocumentKindMap());
            modelBuilder.Configurations.Add(new StatusMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new ActDocumentMap());
            modelBuilder.Configurations.Add(new MotiveDocumentMap());
            modelBuilder.Configurations.Add(new MessageMap());
            modelBuilder.Configurations.Add(new DeletedActMap());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
