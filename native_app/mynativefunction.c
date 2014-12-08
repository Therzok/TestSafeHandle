#include <stdlib.h>

typedef struct test_struct {
	int val;
} test_struct;

#if __GNUC__ >= 4
# define EXTERN(type) extern \
			 __attribute__((visibility("default"))) \
			 type
#elif defined(_MSC_VER)
# define EXTERN(type) __declspec(dllexport) type
#else
# define EXTERN(type) extern type
#endif

EXTERN(void) alloc_test(test_struct **test)
{
	*test = malloc(sizeof(test_struct));
}