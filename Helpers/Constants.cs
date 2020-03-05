using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoresAPI
{
    public static class Constants
    {
        public const string ApiKey = "x-api-key";
        public const string FirebaseUserId = "firebase-user-id";

        public const string WS_GROUP_USUARIOS = "usuarios";
        public const string WS_USUARIOS_AUTENTIFICAR = "login";
        public const string WS_USUARIOS_USUARIOS = "";

        public const string WS_GROUP_CARTERA = "cartera";
        /*
        public const string WS_CARTERA_GRUPOS = "grupos/{usuarioId}";
        public const string WS_CARTERA_GRUPOS_DETALLE = "grupos/detalle/{usuarioId}/{grupoId}";
        */
        public const string WS_CARTERA = "contratosAsesor";
        public const string WS_CARTERA_CONTRATO_DETALLE = "contratoDetalle";
        public const string WS_CARTERA_CREDITO_DETALLE  = "creditoDetalle";


    }
}
