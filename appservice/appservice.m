//
//  smappservice.m
//  smappservice
//
//  Created by Albie Spriddell on 06/06/2025.
//

#import "appservice.h"

#import <ServiceManagement/ServiceManagement.h>

uint8_t convertSMError(NSError* error);

CFTypeRef getMainAppServiceManager(void) {
    return (__bridge CFTypeRef)[SMAppService mainAppService];
}

CFTypeRef createAppServiceFromIdentifier(const char* identifier, uint8_t identifierType) {
    if (identifier == NULL) {
        return NULL;
    }
    
    NSString* str = [NSString stringWithUTF8String:identifier];
    SMAppService* service;
    
    if ([str length] == 0) {
        return NULL;
    }
    
    switch (identifierType) {
        case SERIVCE_TYPE_AGENT:
            service = [SMAppService agentServiceWithPlistName:str];
            break;
            
        case SERVICE_TYPE_DAEMON:
            service = [SMAppService daemonServiceWithPlistName:str];
            break;
            
        case SERVICE_TYPE_LOGINITEM:
            service = [SMAppService loginItemServiceWithIdentifier:str];
            break;
            
        default:
            return NULL;
    }
    
    return (__bridge_retained CFTypeRef)service;
}

void destroyAppService(CFTypeRef smPtr) {
    if (smPtr == NULL) {
        return;
    }
    
    CFRelease(smPtr);
}

uint8_t getServiceStatus(CFTypeRef smPtr) {
    if (smPtr == NULL) {
        return SERVICE_STATUS_NOTFOUND;
    }

    SMAppService* service = (__bridge SMAppService*)smPtr;

    switch ([service status]) {
        case SMAppServiceStatusEnabled:
            return SERVICE_STATUS_ENABLED;
            
        case SMAppServiceStatusNotFound:
            return SERVICE_STATUS_NOTFOUND;
            
        case SMAppServiceStatusNotRegistered:
            return SERVICE_STATUS_NOTREGISTERED;
            
        case SMAppServiceStatusRequiresApproval:
            return SERVICE_STATUS_REQUIRESAPPROVAL;
            
        default:
            return SERVICE_STATUS_UNKNOWN;
    }
}

uint8_t registerService(CFTypeRef smPtr) {
    if (smPtr == NULL) {
        return SERVICE_ERROR_NODETAIL;
    }
    
    NSError* error;
    SMAppService* service = (__bridge SMAppService*)smPtr;

    return [service registerAndReturnError:&error] ? SERVICE_ERROR_NONE : convertSMError(error);
}

uint8_t unregisterService(CFTypeRef smPtr) {
    if (smPtr == NULL) {
        return SERVICE_ERROR_NODETAIL;
    }
    
    NSError* error;
    SMAppService* service = (__bridge SMAppService*)smPtr;

    return [service unregisterAndReturnError:&error] ? SERVICE_ERROR_NONE : convertSMError(error);
}

void openSystemSettings(void) {
    [SMAppService openSystemSettingsLoginItems];
}

uint8_t convertSMError(NSError* error) {
    switch (error.code) {
        case kSMErrorAuthorizationFailure:
            return SERVICE_ERROR_AUTHFAILURE;
        case kSMErrorInternalFailure:
            return SERVICE_ERROR_INTERNALFAILURE;
        case kSMErrorInvalidPlist:
            return SERVICE_ERROR_INVALIDPLIST;
        case kSMErrorInvalidSignature:
            return SERVICE_ERROR_INVALIDSIGNATURE;
        case kSMErrorJobMustBeEnabled:
            return SERVICE_ERROR_JOBMUSTBEENABLED;
        case kSMErrorJobNotFound:
            return SERVICE_ERROR_JOBNOTFOUND;
        case kSMErrorJobPlistNotFound:
            return SERVICE_ERROR_JOBPLISTNOTFOUND;
        case kSMErrorLaunchDeniedByUser:
            return SERVICE_ERROR_LAUNCHDENIEDBYUSER;
        case kSMErrorServiceUnavailable:
            return SERVICE_ERROR_SERVICEUNAVAILABLE;
        case kSMErrorToolNotValid:
            return SERVICE_ERROR_TOOLNOTVALID;
        default:
            return SERVICE_ERROR_NODETAIL;
    }
}
