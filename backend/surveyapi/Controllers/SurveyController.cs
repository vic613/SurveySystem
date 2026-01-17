using Microsoft.AspNetCore.Mvc;
using Survey.Api.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Survey.Api.Controllers;

[ApiController]
[Route("api/survey")]
public class SurveyController : ControllerBase
{
    private readonly IConfiguration _config;

    public SurveyController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("{formId}")]
    public async Task<IActionResult> GetSurveyForm(int formId)
    {
        using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        await conn.OpenAsync();

        // 1. Get form
        var form = await conn.QuerySingleAsync<SurveyFormDto>(
            "SELECT FormID AS FormId, Title FROM tblForm WHERE FormID = @FormID",
            new { FormID = formId });

        // 2. Get fields
        var fields = (await conn.QueryAsync<FormFieldDto>(
            @"SELECT 
                FormFieldID AS FormFieldId,
                FormFieldText AS Question,
                IsRequired
              FROM tblFormField
              WHERE FormID = @FormID
              ORDER BY SortOrder",
            new { FormID = formId })).ToList();

        // 3. Get options
        var options = await conn.QueryAsync<FormFieldOptionDto>(
            @"SELECT 
                FormFieldOptionID AS FormFieldOptionId,
                FormFieldID,
                FormFieldOptionText AS OptionText
              FROM tblFormFieldOption
              WHERE FormFieldID IN @FieldIds",
            new { FieldIds = fields.Select(f => f.FormFieldId) });

        // Attach options to fields
        foreach (var field in fields)
        {
            field.Options = options
                .Where(o => o.FormFieldOptionId != 0 || o.FormFieldOptionId != null)
                .Where(o => o.FormFieldId == field.FormFieldId)
                .ToList();
        }

        form.Fields = fields;
        return Ok(form);
    }

    // Get customer answers (optional)
    [HttpGet("answers/{customerFormId}")]
    public async Task<IActionResult> GetCustomerAnswers(int customerFormId)
    {
        using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        var answers = await conn.QueryAsync<CustomerAnswerDto>(
            @"SELECT
                CustomerFormID,
                FormFieldID,
                FormFieldValue
              FROM tblCustomerFormField
              WHERE CustomerFormID = @CustomerFormID",
            new { CustomerFormID = customerFormId });

        return Ok(answers);
    }
}

