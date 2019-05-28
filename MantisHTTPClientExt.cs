using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MantisBTRestAPIClient
{
    /// <summary>
    /// out of NSwag generator scope, an extension to the generated rest api client
    /// adds workarounds for missing 'oneOf' support avoiding use of polymorphism in schema
    /// </summary>
    public partial class MantisHTTPClient
    {
        /// <summary>
        /// gets an option as a string option
        /// </summary>
        /// <param name="configAnyTypeOption">any type config value option (json)</param>
        /// <returns>json of option value mapped to string value config option</returns>
        public ConfigStringOption GetConfigStringOption(ConfigAnyTypeOption configAnyTypeOption)
        {
            return new ConfigStringOption()
            {
                Option = configAnyTypeOption.Option,
                Value = configAnyTypeOption.Value as string
            };
        }

        /// <summary>
        /// gets an option as an enum option
        /// </summary>
        /// <param name="configAnyTypeOption">any type config value option (json)</param>
        /// <returns>json of enum option value mapped to enum value config option</returns>
        public ConfigEnumOption GetConfigEnumOption(ConfigAnyTypeOption configAnyTypeOption)
        {
            var r = new ConfigEnumOption()
            {
                Option = configAnyTypeOption.Option
            };
            r.Value =
                Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigEnumOptionArray>(
                    configAnyTypeOption.Value.ToString());
            return r;
        }

        /// <summary>
        /// gets an option enum value as a typed enum value option
        /// </summary>
        /// <typeparam name="T">type of the enum value option wrapper from IConfigEnumOption</typeparam>
        /// <param name="configAnyTypeOption">any type config value option (json)</param>
        /// <returns>json of option value mapped to typed enum value option</returns>
        public T GetConfigEnumOption<T>(ConfigAnyTypeOption configAnyTypeOption)
            where T : IConfigEnumOption,new()
        {
            var r = new T();
            var value =
                Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigEnumOptionArray>(
                    configAnyTypeOption.Value.ToString());
            r.InitializeFrom(configAnyTypeOption.Option,value);
            return r;
        }

        /// <summary>
        /// gets option enum values as a typed enum values option
        /// </summary>
        /// <typeparam name="T">type of the enum values option wrapper from IConfigEnumOption,ConfigEnumOption</typeparam>
        /// <param name="optionName">name of the enum option</param>
        /// <param name="projectId">optional project id related to the option</param>
        /// <param name="userId">optional user id related to the option</param>
        /// <returns>json of option enum values mapped to typed enum values option</returns>
        public T ConfigGetEnum<T>(string optionName, int? projectId = null, int? userId = null)
            where T : ConfigEnumOption,IConfigEnumOption,new()
        {
            var r = ConfigGet(new List<string>() { optionName }, projectId, userId);
            return (r == null || r.Configs.Count == 0)
                ? null
                : GetConfigEnumOption<T>(r.Configs.First());
        }

        /// <summary>
        /// gets option enum values as a typed enum values option
        /// </summary>
        /// <typeparam name="T">type of the enum values option wrapper from IConfigEnumOption,ConfigEnumOption</typeparam>
        /// <param name="configOption">config option</param>
        /// <param name="projectId">optional project id related to the option</param>
        /// <param name="userId">optional user id related to the option</param>
        /// <returns>json of option enum values mapped to typed enum values option</returns>
        public T ConfigGetEnum<T>(ConfigOption configOption, int? projectId = null, int? userId = null)
            where T : ConfigEnumOption, IConfigEnumOption, new()
        {
            var r = ConfigGet(new List<string>() { configOption+"" }, projectId, userId);
            return (r == null || r.Configs.Count == 0)
                ? null
                : GetConfigEnumOption<T>(r.Configs.First());
        }

        /// <summary>
        /// gets a config option
        /// </summary>
        /// <param name="optionName">name of the config option</param>
        /// <param name="projectId">optional project id related to the option</param>
        /// <param name="userId">optional user id related to the option</param>
        /// <returns>ConfigGet response object wrapper including any type object (json)</returns>
        public ConfigGetResponse ConfigGet(string optionName, int? projectId = null, int? userId = null)
        {
            return ConfigGet(new List<string>() { optionName }, projectId, userId);
        }

        /// <summary>
        /// async gets a config option
        /// </summary>
        /// <param name="optionName">name of the config option</param>
        /// <param name="projectId">optional project id related to the option</param>
        /// <param name="userId">optional user id related to the option</param>
        /// <param name="cancellationToken"></param>
        /// <returns>task of value ConfigGet response object wrapper including any type object (json)</returns>
        public async Task<ConfigGetResponse> ConfigGetAsync(
            string optionName, int? projectId = null, int? userId = null, CancellationToken cancellationToken = default(CancellationToken) )
        {
            return await ConfigGetAsync(new List<string>() { optionName }, projectId, userId, cancellationToken );
        }

    }

    #region schema extension

    /// <summary>
    /// interface of a config option enum values wrapper class
    /// </summary>
    public interface IConfigEnumOption
    {
        /// <summary>
        /// intialize the wrapper object from an option enum value array
        /// </summary>
        /// <param name="optionName"></param>
        /// <param name="configEnumOption"></param>
        void InitializeFrom(string optionName, ConfigEnumOptionArray configEnumOption);
    }

    /// <summary>
    /// config option 'Priority' value
    /// </summary>
    public class ConfigPriority : ConfigEnumOptionValue
    {
        /// <summary>
        /// constructor from a config enum option value
        /// </summary>
        /// <param name="configEnumOptionValue">config enum option value</param>
        public ConfigPriority(ConfigEnumOptionValue configEnumOptionValue)
        {
            Id = configEnumOptionValue.Id;
            Label = configEnumOptionValue.Label;
            Name = configEnumOptionValue.Name;
        }
    }

    /// <summary>
    /// config option 'Priority' values
    /// </summary>
    public class ConfigPriorities :
        ConfigEnumOption
        , IConfigEnumOption
    {
        /// <summary>
        /// config option 'Priority' values
        /// </summary>
        public IList<ConfigPriority> Priorities { get; set; }
        = new List<ConfigPriority>();

        /// <summary>
        /// constructor from a config enum option value
        /// </summary>
        /// <param name="optionName">name of the option</param>
        /// <param name="configEnumOption">config option enum value</param>
        public void InitializeFrom(
            string optionName,
            ConfigEnumOptionArray configEnumOption)
        {
            Option = optionName;
            foreach (var o in configEnumOption)
            {
                Priorities.Add(
                    new ConfigPriority(o));
            }
        }
    }

    /// <summary>
    /// config option 'Severity' value
    /// </summary>
    public class ConfigSeverity : ConfigEnumOptionValue
    {
        /// <summary>
        /// constructor from a config enum option value
        /// </summary>
        /// <param name="configEnumOptionValue">config enum option value</param>
        public ConfigSeverity(ConfigEnumOptionValue configEnumOptionValue)
        {
            Id = configEnumOptionValue.Id;
            Label = configEnumOptionValue.Label;
            Name = configEnumOptionValue.Name;
        }
    }

    /// <summary>
    /// config option 'Severities' values
    /// </summary>
    public class ConfigSeverities :
        ConfigEnumOption
        , IConfigEnumOption
    {
        /// <summary>
        /// config option 'Severity' values
        /// </summary>
        public IList<ConfigSeverity> Severities { get; set; }
        = new List<ConfigSeverity>();

        /// <summary>
        /// constructor from a config enum option value
        /// </summary>
        /// <param name="optionName">name of the option</param>
        /// <param name="configEnumOption">config option enum value</param>
        public void InitializeFrom(
            string optionName,
            ConfigEnumOptionArray configEnumOption)
        {
            Option = optionName;
            foreach (var o in configEnumOption)
            {
                Severities.Add(
                    new ConfigSeverity(o));
            }
        }
    }

    /// <summary>
    /// config option 'Reproductibility' value
    /// </summary>
    public class ConfigReproductibility : ConfigEnumOptionValue
    {
        /// <summary>
        /// constructor from a config enum option value
        /// </summary>
        /// <param name="configEnumOptionValue">config enum option value</param>
        public ConfigReproductibility(ConfigEnumOptionValue configEnumOptionValue)
        {
            Id = configEnumOptionValue.Id;
            Label = configEnumOptionValue.Label;
            Name = configEnumOptionValue.Name;
        }
    }

    /// <summary>
    /// config option 'Reproductibilities' values
    /// </summary>
    public class ConfigReproductibilities :
        ConfigEnumOption
        , IConfigEnumOption
    {
        /// <summary>
        /// config option 'Reproductibility' values
        /// </summary>
        public IList<ConfigReproductibility> Reproductibilities { get; set; }
        = new List<ConfigReproductibility>();

        /// <summary>
        /// constructor from a config enum option value
        /// </summary>
        /// <param name="optionName">name of the option</param>
        /// <param name="configEnumOption">config option enum value</param>
        public void InitializeFrom(
            string optionName,
            ConfigEnumOptionArray configEnumOption)
        {
            Option = optionName;
            foreach (var o in configEnumOption)
            {
                Reproductibilities.Add(
                    new ConfigReproductibility(o));
            }
        }
    }

    /// <summary>
    /// option names that can be accessed throught the mantis bt rest api
    /// </summary>
    public enum ConfigOption
    {
        access_levels_enum_string,
	    action_button_position,
	    add_bugnote_threshold,
	    add_profile_threshold,
	    admin_site_threshold,
	    allow_account_delete,
	    allow_anonymous_login,
	    allow_blank_email,
	    allow_delete_own_attachments,
	    allow_download_own_attachments,
	    allow_file_upload,
	    allow_freetext_in_profile_fields,
	    allow_no_category,
	    allow_parent_of_unresolved_to_close,
	    allow_permanent_cookie,
	    allow_reporter_close,
	    allow_reporter_reopen,
	    allow_reporter_upload,
	    allow_signup,
	    allowed_files,
	    anonymous_account,
	    antispam_max_event_count,
	    antispam_time_window_in_seconds,
	    assign_sponsored_bugs_threshold,
	    auto_set_status_to_assigned,
	    backward_year_count,
	    bottom_include_page,
	    bug_assigned_status,
	    bug_change_status_page_fields,
	    bug_closed_status_threshold,
	    bug_count_hyperlink_prefix,
	    bug_duplicate_resolution,
	    bug_feedback_status,
	    bug_link_tag,
	    bug_list_cookie,
	    bug_readonly_status_threshold,
	    bug_reminder_threshold,
	    bug_reopen_resolution,
	    bug_reopen_status,
	    bug_report_page_fields,
	    bug_resolution_fixed_threshold,
	    bug_resolution_not_fixed_threshold,
	    bug_resolved_status_threshold,
	    bug_revision_drop_threshold,
	    bug_submit_status,
	    bug_update_page_fields,
	    bug_view_page_fields,
	    bugnote_link_tag,
	    bugnote_order,
	    bugnote_user_change_view_state_threshold,
	    bugnote_user_delete_threshold,
	    bugnote_user_edit_threshold,
	    cdn_enabled,
	    change_view_status_threshold,
	    check_mx_record,
	    complete_date_format,
	    compress_html,
	    cookie_prefix,
	    cookie_time_length,
	    copyright_statement,
	    create_permalink_threshold,
	    create_project_threshold,
	    create_short_url,
	    css_include_file,
	    css_rtl_include_file,
	    csv_columns,
	    csv_separator,
	    custom_field_edit_after_create,
	    custom_field_link_threshold,
	    custom_field_type_enum_string,
	    custom_group_actions,
	    custom_headers,
	    date_partitions,
	    datetime_picker_format,
	    default_bug_additional_info,
	    default_bug_description,
	    default_bug_eta,
	    default_bug_priority,
	    default_bug_projection,
	    default_bug_relationship_clone,
	    default_bug_relationship,
	    default_bug_reproducibility,
	    default_bug_resolution,
	    default_bug_severity,
	    default_bug_steps_to_reproduce,
	    default_bug_view_status,
	    default_bugnote_order,
	    default_bugnote_view_status,
	    default_category_for_moves,
	    default_email_bugnote_limit,
	    default_email_on_assigned_minimum_severity,
	    default_email_on_assigned,
	    default_email_on_bugnote_minimum_severity,
	    default_email_on_bugnote,
	    default_email_on_closed_minimum_severity,
	    default_email_on_closed,
	    default_email_on_feedback_minimum_severity,
	    default_email_on_feedback,
	    default_email_on_new_minimum_severity,
	    default_email_on_new,
	    default_email_on_priority_minimum_severity,
	    default_email_on_priority,
	    default_email_on_reopened_minimum_severity,
	    default_email_on_reopened,
	    default_email_on_resolved_minimum_severity,
	    default_email_on_resolved,
	    default_email_on_status_minimum_severity,
	    default_email_on_status,
	    default_home_page,
	    default_language,
	    default_limit_view,
	    default_manage_tag_prefix,
	    default_new_account_access_level,
	    default_notify_flags,
	    default_project_view_status,
	    default_redirect_delay,
	    default_refresh_delay,
	    default_reminder_view_status,
	    default_show_changed,
	    default_timezone,
	    delete_bug_threshold,
	    delete_bugnote_threshold,
	    delete_project_threshold,
	    development_team_threshold,
	    disallowed_files,
	    display_bug_padding,
	    display_bugnote_padding,
	    display_errors,
	    display_project_padding,
	    download_attachments_threshold,
	    due_date_default,
	    due_date_update_threshold,
	    due_date_view_threshold,
	    email_ensure_unique,
	    email_dkim_domain,
	    email_dkim_enable,
	    email_dkim_identity,
	    email_dkim_selector,
	    email_login_enabled,
	    email_notifications_verbose,
	    email_padding_length,
	    email_receive_own,
	    email_separator1,
	    email_separator2,
	    enable_email_notification,
	    enable_eta,
	    enable_product_build,
	    enable_profiles,
	    enable_project_documentation,
	    enable_projection,
	    enable_sponsorship,
	    eta_enum_string,
	    excel_columns,
	    fallback_language,
	    favicon_image,
	    file_download_content_type_overrides,
	    file_type_icons,
	    file_upload_max_num,
	    filter_by_custom_fields,
	    filter_custom_fields_per_row,
	    filter_position,
	    font_family,
	    font_family_choices,
	    font_family_choices_local,
	    forward_year_count,
	    from_email,
	    from_name,
	    handle_bug_threshold,
	    handle_sponsored_bugs_threshold,
	    hide_status_default,
	    history_default_visible,
	    history_order,
	    html_make_links,
	    html_valid_tags_single_line,
	    html_valid_tags,
	    impersonate_user_threshold,
	    issue_activity_note_attachments_seconds_threshold,
	    language_auto_map,
	    language_choices_arr,
	    limit_email_domains,
	    limit_reporters,
	    logo_image,
	    logo_url,
	    logout_cookie,
	    logout_redirect_page,
	    long_process_timeout,
	    lost_password_feature,
	    main_menu_custom_options,
	    manage_config_cookie,
	    manage_configuration_threshold,
	    manage_custom_fields_threshold,
	    manage_global_profile_threshold,
	    manage_news_threshold,
	    manage_plugin_threshold,
	    manage_project_threshold,
	    manage_site_threshold,
	    manage_user_threshold,
	    manage_users_cookie,
	    max_dropdown_length,
	    max_failed_login_count,
	    max_file_size,
	    max_lost_password_in_progress_count,
	    mentions_enabled,
	    mentions_tag,
	    min_refresh_delay,
	    minimum_sponsorship_amount,
	    monitor_add_others_bug_threshold,
	    monitor_bug_threshold,
	    monitor_delete_others_bug_threshold,
	    move_bug_threshold,
	    my_view_boxes,
	    my_view_bug_count,
	    news_enabled,
	    news_limit_method,
	    news_view_limit_days,
	    news_view_limit,
	    normal_date_format,
	    notify_flags,
	    notify_new_user_created_threshold_min,
	    plugin_mime_types,
	    plugins_enabled,
	    plugins_force_installed,
	    preview_attachments_inline_max_size,
	    preview_image_extensions,
	    preview_max_height,
	    preview_max_width,
	    preview_text_extensions,
	    print_issues_page_columns,
	    priority_enum_string,
	    priority_significant_threshold,
	    private_bug_threshold,
	    private_bugnote_threshold,
	    private_news_threshold,
	    private_project_threshold,
	    project_cookie,
	    project_status_enum_string,
	    project_user_threshold,
	    project_view_state_enum_string,
	    projection_enum_string,
	    reassign_on_feedback,
	    reauthentication_expiry,
	    reauthentication,
	    recently_visited_count,
	    relationship_graph_enable,
	    relationship_graph_fontname,
	    relationship_graph_fontsize,
	    relationship_graph_max_depth,
	    relationship_graph_orientation,
	    relationship_graph_view_on_click,
	    reminder_receive_threshold,
	    reminder_recipients_monitor_bug,
	    reopen_bug_threshold,
	    report_bug_threshold,
	    report_issues_for_unreleased_versions_threshold,
	    reporter_summary_limit,
	    reproducibility_enum_string,
	    resolution_enum_string,
	    resolution_multipliers,
	    return_path_email,
	    roadmap_update_threshold,
	    roadmap_view_threshold,
	    rss_enabled,
	    search_title,
	    set_bug_sticky_threshold,
	    set_configuration_threshold,
	    set_status_threshold,
	    set_view_status_threshold,
	    severity_enum_string,
	    severity_multipliers,
	    severity_significant_threshold,
	    short_date_format,
	    show_assigned_names,
	    show_avatar_threshold,
	    show_avatar,
	    show_bug_project_links,
	    show_changelog_dates,
	    show_detailed_errors,
	    show_log_threshold,
	    show_memory_usage,
	    show_monitor_list_threshold,
	    show_priority_text,
	    show_product_version,
	    show_project_menu_bar,
	    show_queries_count,
	    show_realname,
	    show_roadmap_dates,
	    show_sticky_issues,
	    show_timer,
	    show_user_email_threshold,
	    show_user_realname_threshold,
	    show_version_dates_threshold,
	    show_version,
	    signup_use_captcha,
	    sort_by_last_name,
	    sort_icon_arr,
	    sponsor_threshold,
	    sponsorship_currency,
	    sponsorship_enum_string,
	    status_colors,
	    status_enum_string,
	    status_enum_workflow,
	    status_icon_arr,
	    stop_on_errors,
	    store_reminders,
	    stored_query_create_shared_threshold,
	    stored_query_create_threshold,
	    stored_query_use_threshold,
	    string_cookie,
	    subprojects_enabled,
	    subprojects_inherit_categories,
	    subprojects_inherit_versions,
	    summary_category_include_project,
	    tag_attach_threshold,
	    tag_create_threshold,
	    tag_detach_own_threshold,
	    tag_detach_threshold,
	    tag_edit_own_threshold,
	    tag_edit_threshold,
	    tag_separator,
	    tag_view_threshold,
	    time_tracking_billing_rate,
	    time_tracking_edit_threshold,
	    time_tracking_enabled,
	    time_tracking_reporting_threshold,
	    time_tracking_stopwatch,
	    time_tracking_view_threshold,
	    time_tracking_with_billing,
	    time_tracking_without_note,
	    timeline_view_threshold,
	    top_include_page,
	    update_bug_assign_threshold,
	    update_bug_status_threshold,
	    update_bug_threshold,
	    update_bugnote_threshold,
	    update_readonly_bug_threshold,
	    upload_bug_file_threshold,
	    upload_project_file_threshold,
	    use_dynamic_filters,
	    user_login_valid_regex,
	    validate_email,
	    version_suffix,
	    view_all_cookie,
	    view_attachments_threshold,
	    view_bug_threshold,
	    view_changelog_threshold,
	    view_configuration_threshold,
	    view_filters,
	    view_handler_threshold,
	    view_history_threshold,
	    view_issues_page_columns,
	    view_proj_doc_threshold,
	    view_sponsorship_details_threshold,
	    view_sponsorship_total_threshold,
	    view_state_enum_string,
	    view_summary_threshold,
	    webmaster_email,
	    webservice_admin_access_level_threshold,
	    webservice_error_when_version_not_found,
	    webservice_eta_enum_default_when_not_found,
	    webservice_priority_enum_default_when_not_found,
	    webservice_projection_enum_default_when_not_found,
	    webservice_readonly_access_level_threshold,
	    webservice_readwrite_access_level_threshold,
	    webservice_resolution_enum_default_when_not_found,
	    webservice_rest_enabled,
	    webservice_severity_enum_default_when_not_found,
	    webservice_specify_reporter_on_add_access_level_threshold,
	    webservice_status_enum_default_when_not_found,
	    webservice_version_when_not_found,
	    wiki_enable,
	    wiki_engine_url,
	    wiki_engine,
	    wiki_root_namespace,
	    window_title,
	    wrap_in_preformatted_text
    }

    #endregion

    #region constructors

    /// <summary>
    /// identifier partial class providing supplementary constructors
    /// </summary>
    public partial class Identifier
    {
        /// <summary>
        /// empty constructor
        /// </summary>
        public Identifier() { }

        /// <summary>
        /// constructor from identifier id
        /// </summary>
        /// <param name="id">identifier id</param>
        public Identifier(long id)
        {
            Id = id;
        }
        
        /// <summary>
        /// constructore from identifier name
        /// </summary>
        /// <param name="name">identifier name</param>
        public Identifier(string name)
        {
            Name = name;
        }
    }

    #endregion

}
