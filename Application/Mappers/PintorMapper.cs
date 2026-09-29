using Application.Commands;
using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class PintorMapper
{
    public static Pintor ToEntity(CreatePintorCommand createPintorCommand)
    {
        return new Pintor(createPintorCommand.Nombre, createPintorCommand.Apellido, false);
    }

    public static PintorDTO ToDTO(Pintor pintor)
    {
        return new PintorDTO(pintor.Id, pintor.Nombre, pintor.Apellido);
    }
}
