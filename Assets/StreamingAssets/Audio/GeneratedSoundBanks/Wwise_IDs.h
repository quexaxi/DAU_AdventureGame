/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_MAIN_MUSIC_SWITCH = 1378767680U;
        static const AkUniqueID PLAY_PLAYER_FOOTSTEPS = 98439365U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace AREASTATE
        {
            static const AkUniqueID GROUP = 2064552269U;

            namespace STATE
            {
                static const AkUniqueID DEFAULT = 782826392U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID TEMPLE = 2323193050U;
            } // namespace STATE
        } // namespace AREASTATE

        namespace GAMESTATUS
        {
            static const AkUniqueID GROUP = 1045871717U;

            namespace STATE
            {
                static const AkUniqueID INGAME = 984691642U;
                static const AkUniqueID INMENU = 3374585465U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace GAMESTATUS

        namespace MUSICSTATE
        {
            static const AkUniqueID GROUP = 1021618141U;

            namespace STATE
            {
                static const AkUniqueID EXPLORATION = 2582085496U;
                static const AkUniqueID FINALBOSS = 2147352708U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID SILENCE = 3041563226U;
                static const AkUniqueID TEMPLE = 2323193050U;
            } // namespace STATE
        } // namespace MUSICSTATE

        namespace PLAYERSTATE
        {
            static const AkUniqueID GROUP = 3285234865U;

            namespace STATE
            {
                static const AkUniqueID ALIVE = 655265632U;
                static const AkUniqueID DEAD = 2044049779U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace PLAYERSTATE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace GROUNDMATERIALSWITCH
        {
            static const AkUniqueID GROUP = 1044534455U;

            namespace SWITCH
            {
                static const AkUniqueID DIRT = 2195636714U;
                static const AkUniqueID GRASS = 4248645337U;
                static const AkUniqueID STONE = 1216965916U;
            } // namespace SWITCH
        } // namespace GROUNDMATERIALSWITCH

        namespace PLAYERFOOTSTEPS
        {
            static const AkUniqueID GROUP = 1681012287U;

            namespace SWITCH
            {
                static const AkUniqueID DIRT = 2195636714U;
                static const AkUniqueID GRASS = 4248645337U;
                static const AkUniqueID STONE = 1216965916U;
            } // namespace SWITCH
        } // namespace PLAYERFOOTSTEPS

        namespace PLAYERHEALTH
        {
            static const AkUniqueID GROUP = 151362964U;

            namespace SWITCH
            {
                static const AkUniqueID FULLHEALTH = 2429688720U;
                static const AkUniqueID LOWHEALTH = 1017222595U;
                static const AkUniqueID NEARDEATH = 898449699U;
            } // namespace SWITCH
        } // namespace PLAYERHEALTH

        namespace PLAYERSPEEDSWITCH
        {
            static const AkUniqueID GROUP = 2051106367U;

            namespace SWITCH
            {
                static const AkUniqueID JUMP = 3833651337U;
                static const AkUniqueID RUN = 712161704U;
                static const AkUniqueID WALK = 2108779966U;
            } // namespace SWITCH
        } // namespace PLAYERSPEEDSWITCH

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID RTPC_DISTANCE = 262290038U;
        static const AkUniqueID RTPC_PLAYERSPEED = 2653406601U;
        static const AkUniqueID RTPC_PUZZLEPROGRESS = 4085892778U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID SB_MAIN = 152968626U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMB_BEDS = 1737722166U;
        static const AkUniqueID AMB_BUS = 3319158528U;
        static const AkUniqueID ENVIRONMENT = 1229948536U;
        static const AkUniqueID MASTER_AUDIO_BUS = 2392784291U;
        static const AkUniqueID MECHANISMS = 4015094827U;
        static const AkUniqueID MUSIC_BUS = 2680856269U;
        static const AkUniqueID NPC_BUS = 3319244337U;
        static const AkUniqueID NPC_COMBAT = 2411464625U;
        static const AkUniqueID PLAYER_ATTACK = 2824512041U;
        static const AkUniqueID PLAYER_BUS = 1138681361U;
        static const AkUniqueID PLAYER_COMBAT = 2665470225U;
        static const AkUniqueID PLAYER_DAMAGE = 2074073782U;
        static const AkUniqueID PLAYER_FOOTSTEPS = 1730208058U;
        static const AkUniqueID PLAYER_LOCOMOTION = 1375983526U;
        static const AkUniqueID PROPS = 968010305U;
        static const AkUniqueID SFX_BUS = 213475909U;
        static const AkUniqueID UI_BUS = 3247222208U;
        static const AkUniqueID VO_BUS = 1191351487U;
        static const AkUniqueID WORLD_BUS = 1836527144U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID REV_FOREST = 3910197480U;
        static const AkUniqueID REV_TEMPLE = 472728108U;
        static const AkUniqueID REVERBS_AUX = 4148005607U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
