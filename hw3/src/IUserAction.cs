using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using rolemodel;
public interface IUserAction                           //用户互动会发生的事件
{
    void MoveBoat();                                   //移动船
    void Restart();                                    //重新开始
    void MoveRole(RoleModel role);                     //移动角色
    int Check();                                       //检测游戏结束
}

