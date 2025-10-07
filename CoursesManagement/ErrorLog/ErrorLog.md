# Error Log

This file documents known errors, their causes, and resolutions for the **CoursesManagement** project.  
It is designed to help contributors debug faster and prevent repeating mistakes.


## Error Entries
### ERR-001 – Operator '==' cannot be applied to Guid and int
- **Date:** 2025-10-06  
- **Environment:** Development  
- **Module/Feature:** `CourseRepo` (Repository Layer)  
- **Error Message:** `CS0019: Operator '==' cannot be applied to operands of type 'Guid' and 'int'
File: C:\Users\CodeLine\Desktop\CW\CodelineOnline\CoursesManagement\Repos\CourseRepo.cs
Line: 25`
- **Root Cause:** A comparison was attempted between a `Guid` property and an `int` value. Data types are incompatible.  
- **Resolution/Fix:**  
	- Ensure both operands have the same type.  
	- If the database column is `Guid`, convert the input value using `Guid.Parse()` or update the model/DTO to use `Guid`.  
	- Example fix:  
  ```csharp
  // Wrong
  x.ProgramId == someInt  

  // Correct
  x.ProgramId == Guid.Parse(someStringId);
  ```
- **Status:**  Resolved  
- **Notes:** Likely caused by schema mismatch between EF Core model and request parameters.  


### ERR-002 – Cannot implicitly convert List<T> to IQueryable<T>
- **Date:** 2025-10-06  
- **Environment:** Development  
- **Module/Feature:** `ProgramsRepo` (Repository Layer)  
- **Error Message:** `CS0266: Cannot implicitly convert type
'System.Collections.Generic.List<CoursesManagement.Models.Programs>'
to 'System.Linq.IQueryable<CoursesManagement.Models.Programs>'.
An explicit conversion exists (are you missing a cast?)
File: C:\Users\CodeLine\source\repos\CodelineOnline\CoursesManagement\Repos\ProgramsRepo.cs
Line: 73`

- **Root Cause:**  
The repository method expected an `IQueryable<Programs>` return type, but the query executed with `.ToList()`, which returns a `List<Programs>`.  
- **Resolution/Fix:**  
	- **Option 1:** Change the method’s return type to `List<Programs>` if immediate execution is intended.  
	- **Option 2:** Remove `.ToList()` and return `IQueryable<Programs>` if deferred execution is needed.  
```csharp
// Wrong
IQueryable<Programs> result = _context.Programs.Where(x => x.IsActive).ToList();

// Fix Option 1 (change return type)
List<Programs> result = _context.Programs.Where(x => x.IsActive).ToList();

// Fix Option 2 (keep IQueryable)
IQueryable<Programs> result = _context.Programs.Where(x => x.IsActive);

```
- **Status:** Resolved
- **Notes:** 							
         - Use `IQueryable` when you plan to apply additional filters in services or controllers.
		 - Use `List` if you want to immediately fetch the data.


### ERR-003 – Async method returning wrong type
- **Date:** 2025-10-06  
- **Environment:** Development  
- **Module/Feature:** `ProgramsRepo` (Repository Layer)  
- **Error Message:**  `CS4016: Since this is an async method, the return expression must be of type 'Programs'
rather than 'Task<Programs?>'.
File: C:\Users\CodeLine\source\repos\CodelineOnline\CoursesManagement\Repos\ProgramsRepo.cs
Line: 21`

- **Root Cause:**  
The async method is declared to return `Programs`, but inside the method a `Task<Programs?>` (e.g., from `FirstOrDefaultAsync()` or `FindAsync()`) is being returned directly.  
Async methods must return `Task<T>` (or `Task`) when using `await`.  

- **Resolution/Fix:**  
- If you want an **async method**, change the signature to `Task<Programs?>`.  
- Use `await` when calling EF Core async methods.  
```csharp
// Wrong
public async Programs GetByIdAsync(Guid id)
{
    return _context.Programs.FirstOrDefaultAsync(p => p.ProgramId == id);
}

// Correct
public async Task<Programs?> GetByIdAsync(Guid id)
{
    return await _context.Programs.FirstOrDefaultAsync(p => p.ProgramId == id);
}
```
- **Status:** Resolved
- **Notes:**  
		 - Always match the return type of your method with EF Core’s async operations (ToListAsync, FindAsync, FirstOrDefaultAsync, etc.), which return a Task<T>.


### ERR-004 – Missing service method on interface (method not found on IProgramsService)
- **Date:** 2025-10-06
- **Environment:** Development
- **Module/Feature:** `ProgramsController` (Controller Layer)
- **Error Message:** `CS1061: 'IProgramsService' does not contain a definition for
'GetAllCoursesInProgramByProgramId' and no accessible extension method
'GetAllCoursesInProgramByProgramId' accepting a first argument of type
'IProgramsService' could be found (are you missing a using directive or an assembly reference?)
File: C:\Users\CodeLine\source\repos\CodelineOnline\CoursesManagement\Controllers\ProgramsController.cs
Line: 155`

- **Root Cause:**
The controller calls `IProgramsService.GetAllCoursesInProgramByProgramId(...)`, but that method is **not declared** on the `IProgramsService` interface (and likely not implemented in `ProgramsService`). It could also be a naming mismatch or should belong to `ICoursesService`.
- **Resolution/Fix:**
Choose one of the following consistent contracts and implement it end-to-end:

**Option A – Keep it on ProgramsService**
```csharp
// IProgramsService
Task<List<CourseDto>> GetAllCoursesInProgramByProgramIdAsync(Guid programId);

// ProgramsService
public async Task<List<CourseDto>> GetAllCoursesInProgramByProgramIdAsync(Guid programId)
{
    var courses = await _context.Courses
        .Where(c => c.ProgramCourses.Any(pc => pc.ProgramId == programId))
        .Select(c => new CourseDto { /* map fields */ })
        .ToListAsync();
    return courses;
}

// Controller (ensure async name matches)
var result = await _programsService.GetAllCoursesInProgramByProgramIdAsync(programId);
```

- **Status:** Resolved
- **Notes:**
    - Keep naming Async for async methods. Prefer returning DTOs (CourseDto) from services, not entities.


### ERR-005 – Repository does not implement required interface member
- **Date:** 2025-10-06  
- **Environment:** Development  
- **Module/Feature:** `ProgramsRepo` (Repository Layer)  
- **Error Message:**  `CS0535: 'ProgramsRepo' does not implement interface member
'IProgramsRepo.GetProgramByNameAsync(string)'
File: C:\Users\CodeLine\source\repos\CodelineOnline\CoursesManagement\Repos\ProgramsRepo.cs
Line: 9`

- **Root Cause:**  
The interface `IProgramsRepo` defines a method `GetProgramByNameAsync(string)`, but the `ProgramsRepo` class does not implement it. This causes a compile-time contract violation.  

- **Resolution/Fix:**  
Implement the missing method in the repository:  
```csharp
// In IProgramsRepo
Task<Programs?> GetProgramByNameAsync(string programName);

// In ProgramsRepo
public async Task<Programs?> GetProgramByNameAsync(string programName)
{
    return await _context.Programs
                         .FirstOrDefaultAsync(p => p.ProgramName == programName);
}
```

- **Status:** Resolved
- **Notes:**  
         - If ProgramsService or Controller depends on this method, also verify that DI and usage are aligned (e.g., await _programsRepo.GetProgramByNameAsync(name)).


### ERR-007 – AutoMapper Version Mismatch Error
- **Date:** 2025-10-07  
- **Environment:** Development  
- **Module/Feature:** API Startup / Dependency Injection  
- **Error Message:**  `System.MissingMethodException: Method not found:
'Void AutoMapper.MapperConfiguration..ctor(AutoMapper.MapperConfigurationExpression)'`

- **Root Cause:**  
A version mismatch occurred between **AutoMapper (15.0.1)** and **AutoMapper.Extensions.Microsoft.DependencyInjection (12.0.0)**.  
The DI extension used an outdated constructor signature incompatible with AutoMapper v15, causing a runtime crash during application startup.  

- **Resolution/Fix:**  
- Upgraded `AutoMapper.Extensions.Microsoft.DependencyInjection` to **v15.0.1** to match the main AutoMapper version.  
- Rebuilt and verified the API starts successfully without mapping errors.  
```
Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection -Version 15.0.1
```
- **Status:** Resolved
- **Notes:**  
         - Confirmed AutoMapper registration in Program.cs:
           `builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);`
         - Verified mappings load correctly after rebuild.


### ERR-008 – Pending Model Changes Warning

- **Date:** 2025-10-07  
- **Environment:** Development  
- **Module/Feature:** `CoursesDbContext` / Seed Data  
- **Error Message:**  `An error was generated for warning 'Microsoft.EntityFrameworkCore.Migrations.PendingModelChangesWarning':
The model for context 'CoursesDbContext' changes each time it is built.
This is usually caused by dynamic values used in a 'HasData' call (e.g. new DateTime(), Guid.NewGuid()).
Add a new migration and examine its contents to locate the cause, and replace the dynamic call with a static,
hardcoded value. See https://aka.ms/efcore-docs-pending-changes
.
This exception can be suppressed or logged by passing event ID 'RelationalEventId.PendingModelChangesWarning'
to the 'ConfigureWarnings' method in 'DbContext.OnConfiguring' or 'AddDbContext'.`

- **Root Cause:**  
The `CreatedAt` property in models (`Course`, `Programs`, `Category`) was initialized with `DateTime.UtcNow`,  
which changes each build. EF Core treated this as a model change.  
Static GUIDs were used in seed data, so IDs were **not** the issue.  

- **Resolution/Fix:**  
    - Commented out dynamic seed values (`CreatedAt`) in `SeedData`.  
    - Added suppression in `Program.cs` to ignore the EF Core warning while still keeping runtime defaults:  
  ```csharp
  builder.Services.AddDbContext<CoursesDbContext>(options =>
      options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
             .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning))
  );
  ```

- **Status:** Resolved

- **Notes:**  
    - This approach keeps `CreatedAt = DateTime.UtcNow` working at runtime without polluting seed migrations.  
    - EF Core docs: [PendingModelChangesWarning](https://aka.ms/efcore-docs-pending-changes)  