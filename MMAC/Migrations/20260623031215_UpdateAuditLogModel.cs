using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable

namespace MMAC.Migrations
{
    public partial class UpdateAuditLogModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ၁။ Foreign Key ကို ဘေးကင်းအောင် ဖျက်ခြင်း
            migrationBuilder.Sql(@"
                DO $$ 
                BEGIN 
                    IF EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'fk_auditlogs_traveller_userid') THEN
                        ALTER TABLE ""AuditLogs"" DROP CONSTRAINT ""FK_AuditLogs_Traveller_UserId"";
                    END IF;
                END $$;");

            // ၂။ အကယ်၍ UserId Column ရှိနေသေးလျှင်သာ Rename လုပ်ပါ၊ မရှိလျှင် အသစ်ထည့်ပါ
            migrationBuilder.Sql(@"
                DO $$ 
                BEGIN 
                    IF EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'AuditLogs' AND column_name = 'UserId') THEN
                        ALTER TABLE ""AuditLogs"" RENAME COLUMN ""UserId"" TO ""TravellerId"";
                        ALTER INDEX ""IX_AuditLogs_UserId"" RENAME TO ""IX_AuditLogs_TravellerId"";
                    ELSE
                        IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'AuditLogs' AND column_name = 'TravellerId') THEN
                            ALTER TABLE ""AuditLogs"" ADD COLUMN ""TravellerId"" uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
                            CREATE INDEX ""IX_AuditLogs_TravellerId"" ON ""AuditLogs""(""TravellerId"");
                        END IF;
                    END IF;
                END $$;");

            // ၃။ Foreign Key အသစ် ပြန်ချိတ်ခြင်း
            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Traveller_TravellerId",
                table: "AuditLogs",
                column: "TravellerId",
                principalTable: "Traveller",
                principalColumn: "TravellerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // အပြန်အလှန် ပြန်ပြောင်းခြင်း (Rollback)
            migrationBuilder.DropForeignKey(name: "FK_AuditLogs_Traveller_TravellerId", table: "AuditLogs");
            migrationBuilder.RenameColumn(name: "TravellerId", table: "AuditLogs", newName: "UserId");
            migrationBuilder.RenameIndex(name: "IX_AuditLogs_TravellerId", table: "AuditLogs", newName: "IX_AuditLogs_UserId");
            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Traveller_UserId",
                table: "AuditLogs",
                column: "UserId",
                principalTable: "Traveller",
                principalColumn: "TravellerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}