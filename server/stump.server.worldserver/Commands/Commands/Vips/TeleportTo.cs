using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Commands.Commands.Vips;
using System;
using System.Collections.Generic;

namespace Stump.Server.WorldServer.Commands.Commands
{
    public class TeleportTo : CommandBase
    {

        

        public TeleportTo()
        {






            Aliases = new[] { "teleport" , "goname" };
            RequiredRole = RoleEnum.Moderator;
            Description = "Teleport to the target";
            AddParameter("to", "to", "The character to rejoin", converter: ParametersConverter.CharacterConverter);
            AddParameter("from", "from", "The character that teleport", isOptional: true, converter: ParametersConverter.CharacterConverter);
        }



        public override void Execute(TriggerBase trigger)
        {

            //ulong costoTPKamas = 120000;
            #region Lista de Mapas Permitidos
            List<int> mapasPermitidos = new List<int>();

            mapasPermitidos.Add(152829952);
            mapasPermitidos.Add(190449664);
            mapasPermitidos.Add(193725440);
            mapasPermitidos.Add(121373185);
            mapasPermitidos.Add(163578368);
            mapasPermitidos.Add(94110720);
            mapasPermitidos.Add(87033344);
            mapasPermitidos.Add(96338946);
            mapasPermitidos.Add(146675712);
            mapasPermitidos.Add(17564931);
            mapasPermitidos.Add(104595969);
            mapasPermitidos.Add(87295489);
            mapasPermitidos.Add(87295493);
            mapasPermitidos.Add(87296515);
            mapasPermitidos.Add(5243139);
            mapasPermitidos.Add(64749568);
            mapasPermitidos.Add(96994817);
            mapasPermitidos.Add(96996869);
            mapasPermitidos.Add(96998917);
            mapasPermitidos.Add(66585088);
            mapasPermitidos.Add(166986752);
            mapasPermitidos.Add(98566657);
            mapasPermitidos.Add(157548544);
            mapasPermitidos.Add(116392448);
            mapasPermitidos.Add(106954752);
            mapasPermitidos.Add(176947200);
            mapasPermitidos.Add(22282240);
            mapasPermitidos.Add(116654593);
            mapasPermitidos.Add(79430145);
            mapasPermitidos.Add(174326272);
            mapasPermitidos.Add(149684224);
            mapasPermitidos.Add(149160960);
            mapasPermitidos.Add(157024256);
            mapasPermitidos.Add(181665792);
            mapasPermitidos.Add(72352768);
            mapasPermitidos.Add(155713536);
            mapasPermitidos.Add(107216896);
            mapasPermitidos.Add(101188608);
            mapasPermitidos.Add(118226944);
            mapasPermitidos.Add(157286400);
            mapasPermitidos.Add(66846720);
            mapasPermitidos.Add(22808576);
            mapasPermitidos.Add(27000832);
            mapasPermitidos.Add(40108544);
            mapasPermitidos.Add(27787264);
            mapasPermitidos.Add(17302528);
            mapasPermitidos.Add(107481088);
            mapasPermitidos.Add(96338948);
            mapasPermitidos.Add(55050240);
            mapasPermitidos.Add(18088960);
            mapasPermitidos.Add(132907008);
            mapasPermitidos.Add(174064128);
            mapasPermitidos.Add(149423104);
            mapasPermitidos.Add(89391104);
            mapasPermitidos.Add(56098816);
            mapasPermitidos.Add(102760961);
            mapasPermitidos.Add(56360960);
            mapasPermitidos.Add(21495808);
            mapasPermitidos.Add(57148161);
            mapasPermitidos.Add(125831681);
            mapasPermitidos.Add(59511808);
            mapasPermitidos.Add(176030208);
            mapasPermitidos.Add(104333825);
            mapasPermitidos.Add(182327297);
            mapasPermitidos.Add(26738688);
            mapasPermitidos.Add(66322432);
            mapasPermitidos.Add(62915584);
            mapasPermitidos.Add(182453248);
            mapasPermitidos.Add(61865984);
            mapasPermitidos.Add(62130696);
            mapasPermitidos.Add(57934593);
            mapasPermitidos.Add(123207680);
            mapasPermitidos.Add(179568640);
            mapasPermitidos.Add(110100480);
            mapasPermitidos.Add(110362624);
            mapasPermitidos.Add(109838849);
            mapasPermitidos.Add(109576705);
            mapasPermitidos.Add(112201217);
            mapasPermitidos.Add(119277057);
            mapasPermitidos.Add(140771328);
            mapasPermitidos.Add(169869312);
            mapasPermitidos.Add(169345024);
            mapasPermitidos.Add(169607168);
            mapasPermitidos.Add(176160768);
            mapasPermitidos.Add(182714368);
            mapasPermitidos.Add(184690945);
            mapasPermitidos.Add(187432960);
            mapasPermitidos.Add(187957506);
            mapasPermitidos.Add(195035136);
            mapasPermitidos.Add(152830976);
            mapasPermitidos.Add(190448640);
            mapasPermitidos.Add(193726464);
            mapasPermitidos.Add(121374209);
            mapasPermitidos.Add(163579392);
            mapasPermitidos.Add(94111744);
            mapasPermitidos.Add(87034368);
            mapasPermitidos.Add(96206848);
            mapasPermitidos.Add(146674688);
            mapasPermitidos.Add(17565955);
            mapasPermitidos.Add(104596993);
            mapasPermitidos.Add(87296513);
            mapasPermitidos.Add(87296515);
            mapasPermitidos.Add(88086786);
            mapasPermitidos.Add(5244416);
            mapasPermitidos.Add(64750592);
            mapasPermitidos.Add(96998913);
            mapasPermitidos.Add(96998917);
            mapasPermitidos.Add(96998919);
            mapasPermitidos.Add(66586112);
            mapasPermitidos.Add(166987776);
            mapasPermitidos.Add(98567681);
            mapasPermitidos.Add(157549568);
            mapasPermitidos.Add(116393472);
            mapasPermitidos.Add(106955776);
            mapasPermitidos.Add(176948224);
            mapasPermitidos.Add(22283264);
            mapasPermitidos.Add(116655617);
            mapasPermitidos.Add(79431169);
            mapasPermitidos.Add(174327296);
            mapasPermitidos.Add(149685248);
            mapasPermitidos.Add(149161984);
            mapasPermitidos.Add(157025280);
            mapasPermitidos.Add(181666816);
            mapasPermitidos.Add(72352768);
            mapasPermitidos.Add(155714560);
            mapasPermitidos.Add(107217920);
            mapasPermitidos.Add(101189632);
            mapasPermitidos.Add(118227968);
            mapasPermitidos.Add(157287424);
            mapasPermitidos.Add(66847744);
            mapasPermitidos.Add(22806530);
            mapasPermitidos.Add(27003904);
            mapasPermitidos.Add(40109568);
            mapasPermitidos.Add(27789312);
            mapasPermitidos.Add(17304576);
            mapasPermitidos.Add(107483136);
            mapasPermitidos.Add(96338950);
            mapasPermitidos.Add(55050242);
            mapasPermitidos.Add(18089984);
            mapasPermitidos.Add(132908032);
            mapasPermitidos.Add(174065664);
            mapasPermitidos.Add(149424128);
            mapasPermitidos.Add(89392128);
            mapasPermitidos.Add(56099840);
            mapasPermitidos.Add(102761985);
            mapasPermitidos.Add(56361984);
            mapasPermitidos.Add(21499904);
            mapasPermitidos.Add(57149697);
            mapasPermitidos.Add(125830659);
            mapasPermitidos.Add(59512832);
            mapasPermitidos.Add(175899136);
            mapasPermitidos.Add(104334849);
            mapasPermitidos.Add(182326273);
            mapasPermitidos.Add(26739712);
            mapasPermitidos.Add(66323456);
            mapasPermitidos.Add(62916608);
            mapasPermitidos.Add(182454272);
            mapasPermitidos.Add(61867008);
            mapasPermitidos.Add(62131720);
            mapasPermitidos.Add(57935617);
            mapasPermitidos.Add(123208704);
            mapasPermitidos.Add(179569664);
            mapasPermitidos.Add(110101504);
            mapasPermitidos.Add(110363648);
            mapasPermitidos.Add(109839873);
            mapasPermitidos.Add(109577729);
            mapasPermitidos.Add(112202241);
            mapasPermitidos.Add(119276035);
            mapasPermitidos.Add(140772352);
            mapasPermitidos.Add(169870336);
            mapasPermitidos.Add(169346048);
            mapasPermitidos.Add(169608192);
            mapasPermitidos.Add(176161792);
            mapasPermitidos.Add(182715392);
            mapasPermitidos.Add(184689921);
            mapasPermitidos.Add(187433984);
            mapasPermitidos.Add(187957508);
            mapasPermitidos.Add(195036160);
            mapasPermitidos.Add(160564224);
            mapasPermitidos.Add(162004992);
            mapasPermitidos.Add(143917569);
            mapasPermitidos.Add(143138823);
            mapasPermitidos.Add(137101312);
            mapasPermitidos.Add(136840192);
            mapasPermitidos.Add(129500160);
            mapasPermitidos.Add(130286592);

            #endregion





            var to = trigger.Get<Character>("to");
            Character from;



            if (trigger.IsArgumentDefined("from"))
                from = trigger.Get<Character>("from");
            else if (trigger is GameTrigger)
                from = (trigger as GameTrigger).Character;
            else
            {
                throw new Exception("Character to teleport not defined !");
            }



            if (from.UserGroup.Role == RoleEnum.Administrator)
            {
                from.Teleport(to.Position);
            }

            #region Condicion Sí no esta En Mazmorra
           else if (!to.Map.IsDungeon() )
            {
                if (from.IsInParty() )
                {
                    if (!to.IsInFight())
                    {
                        if (to.IsInParty())
                        {
                            if (to.UserGroup.Role == RoleEnum.Administrator)
                            {
                                (trigger as GameTrigger).Character.DisplayNotification("Sí Requieres un de un administrador, ponte en contacto");
                                return;
                            }
                            else
                            {
                                from.Teleport(to.Position);
                                (trigger as GameTrigger).Character.DisplayNotification("Has Sido Teletransportado al jugador  " + to.Name);
                                return;
                            }
                            
                        }
                        
                    }
                    else
                    {

                        (trigger as GameTrigger).Character.DisplayNotification("No puedes usar este comando porque el personaje destino esta en Pelea");
                        return;
                    }

                }
                else
                {
                    (trigger as GameTrigger).Character.DisplayNotification("No puedes usar este comando porque el personaje destino no esta en tu grupo.");
                    return;
                }
            }

            #endregion

            #region Condicion Sí esta en mazmorra

            else if (to.Map.IsDungeon() )
            {
                if (mapasPermitidos.Contains(to.Map.Id))
                {
                    if (from.IsInParty())
                    {
                        if (!to.IsInFight())
                        {
                            if (to.IsInParty())
                            {
                                if (to.UserGroup.Role == RoleEnum.Administrator)
                                {
                                    (trigger as GameTrigger).Character.DisplayNotification("Sí Requieres un de un administrador, ponte en contacto");
                                    return;
                                }
                                else
                                {
                                    from.Teleport(to.Position);
                                    //from.Inventory.SubKamas(costoTPKamas);



                                    (trigger as GameTrigger).Character.DisplayNotification("Has Sido Teletransportado al jugador  " + to.Name + " Que se encuentra dentro de una Mazmorra, por lo tanto se te han descontado:   0 Kamas.");
                                    return;
                                }
                                

                            }
                            else {
                                (trigger as GameTrigger).Character.DisplayNotification("No puedes usar este comando porque el personaje destino no esta en tu grupo.");
                                return;
                            }
                        }
                        else
                        {

                            (trigger as GameTrigger).Character.DisplayNotification("No puedes usar este comando porque el personaje destino esta en Pelea");
                            return;
                        }

                    }
                    else
                    {
                        (trigger as GameTrigger).Character.DisplayNotification("No puedes usar este comando porque el personaje destino no esta en tu grupo.");
                        return;
                    }

                }

                else
                {
                    (trigger as GameTrigger).Character.DisplayNotification("No puedes usar este comando porque el personaje destino esta en mapa no permitido.");
                    return;
                }

            }
            

            #endregion

           


           
        }

    }
}