using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Repositories.Entity;

namespace TeamUp.ModelViews.RoomPlayerModelViews
{
    public class CreateRoomPlayerModelView
    {
        [Required(ErrorMessage = "Mã phòng không được để trống")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Người chơi không được để trống")]
        public int PlayerId { get; set; }
    }
}
