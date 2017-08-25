 /** Author:       Plyashkevich Viatcheslav <plyashkevich@yandex.ru>
 
  This program is free software; you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation; either version 2 of the License, or
  (at your option) any later version.
 --------------------------------------------------------------------------
  * All rights reserved.
  */

using System;

// DtmfGenerator and DtmfDetector are included in C# project

public class DtmfExample
{
    const short FRAME_SIZE = 160;
    
    public DtmfExample() 
    {
    }
    
    
    public static void Main(string [] args)
    {        
        short [] shortsArray = new short[FRAME_SIZE];
        DtmfGenerator dtmfGenerator =     new DtmfGenerator(FRAME_SIZE, 40, 20); 
        DtmfDetector dtmfDetector   =     new DtmfDetector(FRAME_SIZE);
        char [] dialButtons = new char[DtmfGenerator.NUMBER_BUTTONS];
        dialButtons[0] =  '7';
        dialButtons[1] =  '7';
        dialButtons[2] =  '7';
        dialButtons[3] =  '1';
        dialButtons[4] =  '2';
        dialButtons[5] =  '3';
        dialButtons[6] =  'A';
        dialButtons[7] =  '4';
        dialButtons[8] =  '5';
        dialButtons[9] =  '6';
        dialButtons[10] = 'B';
        dialButtons[11] = '7';
        dialButtons[12] = '8';
        dialButtons[13] = '9';
        dialButtons[14] = 'C';
        dialButtons[15] = '*';
        dialButtons[16] = '0';
        dialButtons[17] = '#';
        dialButtons[18] = '#';
        dialButtons[19] = '#';
        
                
        
        Console.Write("Begin program");
        
        for(int cnt = 0; cnt < 4; ++cnt)            
        {
            Console.WriteLine("");
            // Installation symbols for following generation
            dtmfGenerator.transmitNewDialButtonsArray(dialButtons, 20);       
            while(!dtmfGenerator.getReadyFlag())
            { 
                // 8 kHz, 16 bit's PCM frame's generation
                dtmfGenerator.dtmfGenerating(shortsArray);            

                // 8 kHz, 16 bit's PCM frame's detection
                dtmfDetector.dtmfDetecting(shortsArray);
                if(dtmfDetector.getIndexDialButtons() > 0)
                {
                    char [] buttons = dtmfDetector.getDialButtonsArray();
                    for(int ii = 0; ii < dtmfDetector.getIndexDialButtons(); ++ii)
                        Console.Write(buttons[ii]);
                    dtmfDetector.zerosIndexDialButtons();                                    
                }                
            }
        }

        Console.WriteLine("");
        Console.WriteLine("End program ");
       //----------------------------------------------------------------------------------------- 
    }  
}
