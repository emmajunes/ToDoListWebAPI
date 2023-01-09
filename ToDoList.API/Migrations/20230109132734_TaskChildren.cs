using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.API.Migrations
{
    public partial class TaskChildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToDoListDtoId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ToDoListDtoId",
                table: "Tasks",
                column: "ToDoListDtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_ToDoList_ToDoListDtoId",
                table: "Tasks",
                column: "ToDoListDtoId",
                principalTable: "ToDoList",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_ToDoList_ToDoListDtoId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ToDoListDtoId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ToDoListDtoId",
                table: "Tasks");
        }
    }
}
