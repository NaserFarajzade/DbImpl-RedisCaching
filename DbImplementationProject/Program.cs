
using DbImplementationProject;

var utils = new Utils();


var searchListWithDuplicate = utils.GetRandomSearchListWithDuplicate(
    100000,
    3000, 
    50, 
    70000, 
    200
    );

utils.Query(searchListWithDuplicate);
