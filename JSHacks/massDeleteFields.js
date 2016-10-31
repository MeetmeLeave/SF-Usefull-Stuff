function massSoftDeleteFields() {
    var names =
        `tttwer__c
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
            var linkName = titleSplit[titleSplit.length - 1];
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
            var hrefName = 'temp';
            try {
                hrefName = delLink.attributes.onClick.value.split('ustomFieldConfirmDeletePage?name=')[1].split('\'')[0];
            } catch (err) {
                console.log('Error: ' + err);
            }
            var xmlhttp = new XMLHttpRequest();
            xmlhttp.open("GET", href, true);
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4) {
                    if (xmlhttp.status == 200) {
                        console.log(hrefName + ' deleted!');
                    } else {
                        console.log('Failed to delete field: ' + hrefName);
                    }

                    that.i += 1;
                    that.performCall();
                }
            }.bind(that);
            xmlhttp.send();
        } else {
            console.log('Deletion completed!');
        }
    }, 3500);
}