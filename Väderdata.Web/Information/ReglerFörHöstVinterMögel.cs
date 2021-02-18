using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Väderdata.Web
{
    public class ReglerFörHöstVinterMögel
    {
        /*
         Höst
        -- Den meteorologiska definitionen av höst är att dygnsmedeltemperaturen ska vara sjunkande och lägre än 10,0 plusgrader men högre än 0,0°.
        -- Om dygnsmedeltemperaturen är lägre än 10,0°C fem dygn i följd, säger vi att hösten anlände det första av dessa dygn.
        -- Även om det blir en återgång till högre temperaturer därefter så räknas det fortfarande som höst.
        -- En beräkning av dygnsmedeltemperaturen kan resultera i ett antal decimaler, men man gör en avrundning till en tiondels grad innan man tillämpar
           definitionen av de olika årstiderna
        -- Som ytterligare villkor gäller att hösten inte kan börja före den 1 augusti. Det är någon vecka efter medeldatum för årets varmaste dag.


        Vinter
        -- Meteorologer definierar vinter som den period då dygnets medeltemperatur varaktigt är 0,0 grader eller lägre.
        -- Om dygnsmedeltemperaturen är 0,0°C eller lägre fem dygn i följd, säger vi att vintern anlände det första av dessa dygn.
        -- Även om det blir en återgång till högre temperaturer därefter så räknas det fortfarande som vinter.
        -- I vår definition av höstens ankomst har vi satt den 1 augusti som tidigast möjliga datum och naturligtvis kan inte heller vintern komma tidigare än så.
        -- I det svenska klimatet är det praktiskt taget alltid höst under en längre eller kortare period innan vintern anländer. Men skulle villkoret för vinter uppfyllas innan 
            eller samtidigt som villkoret för höst är uppfyllt, så skulle det rent teoretiskt kunna bli vinter utan föregående höst.
        -- I enstaka fall har det  inträffat att villkoret för vinter uppfyllts först när våren redan har anlänt. I så fall tillåts inte årstiderna att backa utan det förblir vår.
        -- En beräkning av dygnsmedeltemperaturen kan resultera i ett antal decimaler, men man gör en avrundning till en tiondels grad innan man tillämpar definitionen av de 
            olika årstiderna. Exempelvis avrundas 0,02° till 0,0°.
         

        Mögel Risk Definition
        --  http://www.penthon.com/vanliga-fragor/faq/vad-innebar-mogelindex/
        -- temperatur från 0 till 50 grader
        -- fukt under 78% för torrt
        -- Python formula för fukt

        rtemp = relative temperature
        rhum = relative humidity


                rtemp = round(temp)
                rhum = round(hum)
                if (rtemp <= 0) or (rtemp > 50):
                     mindex = 0
                else:
                     for i in range(1,4):
                          if (rhum < mtab[rtemp][i]):
                               mindex = i-1
                               break
                     else:
                          mindex = 3
                print (mindex)

         */

    }
}
