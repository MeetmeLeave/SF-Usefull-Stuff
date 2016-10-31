function massSoftDeleteFields() {
    var names =
        `tt_del_del__c
aa_del_del__c
`;

    var namesArray = names.split('\n');
    var links = document.getElementsByTagName('a');

    var delLinks = new Array();
    for (var i = 0; i < links.length; i++) {
        var link = links[i];

        if (link.innerHTML == "Del") {
            var elements = link.parentNode.childNodes;
            var element;
            for (var j = 0; j < elements.length; j++) {
                if (elements[j].innerHTML == 'Edit') {
                    element = elements[j];
                    break;
                }
            }
            var titleSplit = element.title.split(' ');
            var linkName = titleSplit[titleSplit.length - 1]; //link.getAttribute('onClick').split('/ui/setup/confirm/CustomFieldConfirmDeletePage?name=')[1].split('\'')[0];
            if (namesArray.indexOf(linkName) >= 0) {
                delLinks[delLinks.length] = link;
            }
        }
    }

    var exec = new deletionExecutor(delLinks);
    exec.performCall();
}

function deletionExecutor(linksToDelete) {
    this.i = 0;
    this.delLinks = linksToDelete;
}

deletionExecutor.prototype.performCall = function () {
    setTimeout(() => {
        if (this.i < this.delLinks.length) {
            var that = this;
            var delLink = this.delLinks[this.i];

            var href = delLink.attributes.href.value;
            var xmlhttp = new XMLHttpRequest();
            // xmlhttp.open("GET", '/setup/own/deleteredirect.jsp?type=ContactCleanInfo&setupid=ContactCleanInfoFields&delID=00N0Y00000486TS&retURL=%2Fp%2Fsetup%2Flayout%2FLayoutFieldList%3Ftype%3DContactCleanInfo%26setupid%3DContactCleanInfoFields&_CONFIRMATIONTOKEN=VmpFPSxNakF4TmkweE1DMHpNRlF5TWpveU1Ub3dOUzQxT1RCYSxISTN1aWlCUkxqemFoSkR0cnZreWZQLFl6UmhPRE16', true);
            xmlhttp.open("GET", href, true);
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4) {
                    if (xmlhttp.status == 200) {
                        console.log('deleted!');
                    } else {
                        console.log('Failed to delete field: ' + linkName2);
                    }

                    that.i += 1;
                    that.performCall();
                }
            }.bind(that);
            xmlhttp.send();
        }
    }, 3500);
}