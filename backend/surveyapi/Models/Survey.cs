namespace Survey.Api.Models;

public class Survey
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}

public class SurveyFormDto
{
    public int FormId { get; set; }
    public string Title { get; set; }
    public List<FormFieldDto> Fields { get; set; }
}

public class FormFieldDto
{
    public int FormFieldId { get; set; }
    public string Question { get; set; }
    public string FieldType { get; set; }
    public bool IsRequired { get; set; }
    public List<FormFieldOptionDto> Options { get; set; }
}

public class CustomerAnswerDto
{
    public int CustomerFormId { get; set; }
    public int FormFieldId { get; set; }
    public string FormFieldValue { get; set; } // JSON string
}


public class FormFieldOptionDto
{
    public int FormFieldOptionId { get; set; }
    public int FormFieldId { get; set; }   // ✅ REQUIRED
    public string OptionText { get; set; }
}

