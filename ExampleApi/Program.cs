using ExampleApi.DataAccess.UnitOfWork;
using ExampleApi.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder => builder.WithOrigins("http://localhost:5103").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

#region UserMethods

app.MapGet("GetUserById/{id}", (int id) =>
 {
     using (var uow = new UnitOfWork())
     {
         var data = uow.GetRepository<User>().Get(x => x.Id.Equals(id));
         if (data != null) return Results.Ok(data);
         else return Results.NotFound("Veri bulunamadı.");
     }
 });
app.MapGet("GetAllUsers", () =>
{
    using (var uow = new UnitOfWork())
    {
        var data = uow.GetRepository<User>().GetAll();
        return Results.Ok(data.ToList());
    }
});
app.MapPost("AddUser", ([FromBody] User model) =>
{
    using (var uow = new UnitOfWork())
    {
        uow.GetRepository<User>().Add(model);
        if (uow.SaveChanges() > 0) return Results.Ok(model);
        else return Results.StatusCode(500);
    }
});
app.MapPut("UpdateUser", ([FromBody] User model) =>
{
    using (var uow = new UnitOfWork())
    {
        if (model == null) return Results.BadRequest();

        uow.GetRepository<User>().Update(model);
        if (uow.SaveChanges() > 0) return Results.Ok(model);
        else return Results.StatusCode(500);

    }
});
app.MapDelete("DeleteUser", ([FromBody] User model) =>
{
    using (var uow = new UnitOfWork())
    {
        if (model == null) return Results.BadRequest();
        var data = uow.GetRepository<User>().Get(x => x.Id.Equals(model.Id));
        if (data != null)
            uow.GetRepository<User>().Delete(data);
        else return Results.NotFound("Kullanıcı Bulunumadı");
        if (uow.SaveChanges() > 0) return Results.Ok(data);
        return Results.StatusCode(500);
    }
});
#endregion

#region ActivityMethods
app.MapGet("GetAllActivity", () =>
{
    using (var uow = new UnitOfWork())
    {
        try
        {
            var data = uow.GetRepository<Activity>().GetAll();
            if (data == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(data.ToList());

        }
        catch (global::System.Exception ex)
        {
            return Results.StatusCode(500);
        }

    }
});
app.MapGet("GetActivityById/{id}", (int id) =>
{
    using (var uow = new UnitOfWork())
    {
        try
        {
            var data = uow.GetRepository<Activity>().Get(x => x.Id.Equals(id));
            if (data == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(data);

        }
        catch (global::System.Exception)
        {
            return Results.StatusCode(500);
        }
    }
});
app.MapPost("AddActivity", ([FromBody] Activity model) =>
{
    using (var uow = new UnitOfWork())
    {
        try
        {
            uow.GetRepository<Activity>().Add(model);
            if (uow.SaveChanges() > 0)
                return Results.Ok(model);
            else return Results.StatusCode(500);
        }
        catch (global::System.Exception)
        {
            return Results.StatusCode(500);
        }
    }
});
app.MapPut("UpdateActivity", ([FromBody] Activity model) =>
{
    using (var uow = new UnitOfWork())
    {
        try
        {
            if (model == null) return Results.BadRequest();
            uow.GetRepository<Activity>().Update(model);
            if (uow.SaveChanges() > 0) return Results.Ok();
            else return Results.StatusCode(500);
        }
        catch (global::System.Exception)
        {
            return Results.StatusCode(500);
        }
    }
});
app.MapDelete("DeleteActivity/{id}", (int id) =>
{
    using (var uow = new UnitOfWork())
    {

        try
        {
            var model = uow.GetRepository<Activity>().Get(x => x.Id.Equals(id));
            if (model == null) return Results.BadRequest();
            uow.GetRepository<Activity>().Delete(model);
            if (uow.SaveChanges() > 0) return Results.Ok();
            else return Results.StatusCode(500);
        }
        catch (global::System.Exception)
        {
            return Results.StatusCode(500);
        }
    }
});

#endregion

#region PlanService

app.MapGet("GetPlanById/{id}", (int id) =>
{
    using (var uow = new UnitOfWork())
    {
        try
        {
            var data = uow.GetRepository<Plan>().Get(x => x.Id.Equals(id));
            if (data == null) return Results.NotFound();

            return Results.Ok(data);

        }
        catch (global::System.Exception)
        {
            return Results.StatusCode(500);
        }
    }
});
app.MapGet("GetAllPlan", () =>
{
    using (var uow = new UnitOfWork())
    {
        try
        {
            var data = uow.GetRepository<Plan>().GetAll();
            if (data == null) return Results.NotFound();

            return Results.Ok(data.ToList());

        }
        catch (global::System.Exception ex)
        {
            return Results.StatusCode(500);
        }

    }
});
app.MapPost("AddPlan", ([FromBody] Plan model) =>
{
    using (var uow = new UnitOfWork())
    {
        try
        {
            uow.GetRepository<Plan>().Add(model);
            if (uow.SaveChanges() > 0) return Results.Ok(model);

            else return Results.StatusCode(500);
        }
        catch (global::System.Exception)
        {
            return Results.StatusCode(500);
        }
    }
});
app.MapPut("UpdatePlan", ([FromBody] Plan model) =>
{
    using (var uow = new UnitOfWork())
    {
        try
        {
            if (model == null) return Results.BadRequest();

            uow.GetRepository<Plan>().Update(model);

            if (uow.SaveChanges() > 0) return Results.Ok();

            else return Results.StatusCode(500);
        }
        catch (global::System.Exception)
        {
            return Results.StatusCode(500);
        }
    }
});
app.MapDelete("DeletePlan/{id}", (int id) =>
{
    using (var uow = new UnitOfWork())
    {
        try
        {
            var model = uow.GetRepository<Plan>().Get(x => x.Id.Equals(id));
            if (model == null) return Results.BadRequest();

            uow.GetRepository<Plan>().Delete(model);

            if (uow.SaveChanges() > 0) return Results.Ok();

            else return Results.StatusCode(500);
        }
        catch (global::System.Exception)
        {
            return Results.StatusCode(500);
        }
    }
});
#endregion

app.Run();
