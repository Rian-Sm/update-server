# Define the new cron job
new_cron_job="* */5 * * * `pwd`/Service.sh"

# Check if the cron job already exists
(crontab -l | grep -F "$new_cron_job") || {

    # If the cron job does not exist, add it
    (crontab -l; echo "$new_cron_job") | crontab -
    echo "New cron job added: $new_cron_job"
}

new_cron_job_two="@reboot `pwd`/Service.sh"

(crontab -l | grep -F "$new_cron_job_two") || {

    (crontab -l; echo "$new_cron_job_two") | crontab -
    echo "New cron job added: $new_cron_job_two"
}

