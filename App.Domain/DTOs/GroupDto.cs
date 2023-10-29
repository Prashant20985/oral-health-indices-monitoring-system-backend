namespace App.Domain.DTOs;

public class GroupDto
{
    public Guid Id { get; set; }

    public string GroupName { get; set; }

    public List<StudentDto> Students { get; set; }
}
