$(function () {
    $('body').on('click', 'a.review-helpful, a.review-unhelpful', function () {
        var $this = $(this);
        
        var id = $this.attr('data-review-id');
        var helpful = $this.hasClass('review-helpful');

        $.ajax({
            type: 'POST',
            url: '/Reviews/' + id + '/Feedback',
            data: { Helpful: helpful },
            success: function (response) {
                $this.parent().html(response);
            }
        });
    });

    $('body').on('click', '.submit-report', function() {

        var name = $(this).data("group");
        var radio = $("input:radio[name=" + name + "]:checked");

        if (radio.length != 0) {
            openModal(radio.data("location"));
        }
    });
});
