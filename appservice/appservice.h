//
//  smappservice.h
//  smappservice
//
//  Created by Albie Spriddell on 06/06/2025.
//

#import <Foundation/Foundation.h>

#define SERIVCE_TYPE_AGENT 1
#define SERVICE_TYPE_DAEMON 2
#define SERVICE_TYPE_LOGINITEM 3

#define SERVICE_STATUS_NOTREGISTERED 0
#define SERVICE_STATUS_ENABLED 1
#define SERVICE_STATUS_REQUIRESAPPROVAL 2
#define SERVICE_STATUS_NOTFOUND 3
#define SERVICE_STATUS_UNKNOWN 4

#define SERVICE_ERROR_NONE 0
#define SERVICE_ERROR_NODETAIL 1
#define SERVICE_ERROR_AUTHFAILURE 2
#define SERVICE_ERROR_INTERNALFAILURE 3
#define SERVICE_ERROR_INVALIDPLIST 4
#define SERVICE_ERROR_INVALIDSIGNATURE 5
#define SERVICE_ERROR_JOBMUSTBEENABLED 6
#define SERVICE_ERROR_JOBNOTFOUND 7
#define SERVICE_ERROR_JOBPLISTNOTFOUND 8
#define SERVICE_ERROR_LAUNCHDENIEDBYUSER 9
#define SERVICE_ERROR_SERVICEUNAVAILABLE 10
#define SERVICE_ERROR_TOOLNOTVALID 11

FOUNDATION_EXPORT CFTypeRef getMainAppServiceManager(void);
FOUNDATION_EXPORT CFTypeRef createAppServiceFromIdentifer(const char* identifier, uint8_t identifierType);
FOUNDATION_EXPORT void destroyAppService(CFTypeRef smPtr);

// methods return a SERVICE_STATUS_* value
FOUNDATION_EXPORT uint8_t getServiceStatus(CFTypeRef smPtr);

// methods return a SERVICE_ERROR_* value
FOUNDATION_EXPORT uint8_t registerService(CFTypeRef smPtr);
FOUNDATION_EXPORT uint8_t unregisterService(CFTypeRef smPtr);

FOUNDATION_EXPORT void openSystemSettings(void);
