using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Utils
{
    public class SweetAlertHelper
    {
        public static string Mensaje(string titulo, string mensaje, SweetAlertMessageType tipoMensaje)
        {
            return "swal({title: '" + titulo + "',text: '" + mensaje + "',type: '" + tipoMensaje + "'});";
        }
        public static string isActivo(string titulo, string mensaje, SweetAlertMessageType tipoMensaje)
        {
            return "swal({title: '" + titulo + "',text: '" + mensaje + "',type: '" + tipoMensaje + "',showConfirmButton: true}).then(function(){window.location.reload();});";
            //swal({title: "Good job!", text: "Thanks !",type: "success",showConfirmButton: true}).then(function(){window.location.reload();})
        }
    }

    public enum SweetAlertMessageType
    {
        warning, error, success, info
    }
}
