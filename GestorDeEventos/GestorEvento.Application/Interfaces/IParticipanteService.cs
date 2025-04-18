﻿using GestorEvento.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Application.Interfaces
{
    public interface IParticipanteService
    {
        Task RegistrarParticipanteAsync(RegistrarParticipanteDTO dto);
    }

}
