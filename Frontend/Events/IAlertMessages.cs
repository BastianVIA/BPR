﻿namespace Frontend.Events;

public interface IAlertMessages
{
    public Alert NetworkError();
    public Alert ActuatorDetailsSuccess();
    public Alert ActuatorDetailsFailure();
    
}